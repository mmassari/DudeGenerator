using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace DudeGenerator
{
	public class Method :  TypeBase
	{
		public string Body { get; set; }
		public override MethodDeclarationSyntax Generate()
		{
			StatementSyntax syntax = null;
			if(!string.IsNullOrWhiteSpace(Body))
				syntax = SyntaxFactory.ParseStatement(Body);

			// Create a method
			return SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName(Type), Name)
				 .AddModifiers(SyntaxFactory.Token(IsPublic ? SyntaxKind.PublicKeyword : SyntaxKind.PrivateKeyword))
				 .WithBody(SyntaxFactory.Block(syntax));
		}
	}
}
