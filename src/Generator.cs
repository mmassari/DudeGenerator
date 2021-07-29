using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DudeGenerator
{
	public class Generator
	{
		public List<NamespaceDeclarationSyntax> NamespaceList { get; set; }
		public override string ToString()
		{
			return String.Concat(NamespaceList.Select(c => c.NormalizeWhitespace().ToFullString()));
		}
		public void ToFile(string filename)
		{
			System.IO.File.WriteAllText(filename, ToString());
		}
	}
}
