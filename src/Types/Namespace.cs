using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DudeGenerator
{
	public class Namespace
	{
		public string Name { get; set; }
		public List<string> UsingList { get; set; }
		public List<Class> ClassList { get; set; }
		public Namespace()
		{
			ClassList = new List<Class>();
			UsingList = new List<string>();
		}
		public Namespace AddUsing(params string[] _using)
		{
			UsingList.AddRange(_using.Except(UsingList));
			return this;
		}

		public NamespaceDeclarationSyntax Generate()
		{
			var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(Name)).NormalizeWhitespace();
			@namespace = @namespace.AddUsings(UsingList.Select(c => SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(c))).ToArray());
			@namespace = @namespace.AddMembers(ClassList.Select(c => c.Generate()).Cast<MemberDeclarationSyntax>().ToArray());
			return @namespace;
		}
	}
}