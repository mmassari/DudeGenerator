using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DudeGenerator
{
	public class Field : TypeBase
	{
		public override FieldDeclarationSyntax Generate()
		{
			// Create a string variable: (bool canceled;)
			var variableDeclaration = SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName(Type))
				 .AddVariables(SyntaxFactory.VariableDeclarator(Name));

			// Create a field declaration: (private bool canceled;)
			return SyntaxFactory.FieldDeclaration(variableDeclaration)
				 .AddModifiers(SyntaxFactory.Token(IsPublic ? SyntaxKind.PublicKeyword : SyntaxKind.PrivateKeyword));
		}
	}
}