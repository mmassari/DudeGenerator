using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace DudeGenerator
{
	public class Property :  TypeBase
	{
		public bool ReadOnly { get; set; }

		public override PropertyDeclarationSyntax Generate()
		{
			var accessors = new List<AccessorDeclarationSyntax>();
			accessors.Add(SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
				.WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));

			if (!ReadOnly)
				accessors.Add(SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
					.WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));

			return SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(Type), Name)
				.AddModifiers(SyntaxFactory.Token(IsPublic ? SyntaxKind.PublicKeyword : SyntaxKind.PrivateKeyword))
				.AddAccessorListAccessors(accessors.ToArray());
		}
	}
}
