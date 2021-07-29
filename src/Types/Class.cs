using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DudeGenerator
{
	public interface IMaker
	{
		string Name { get; set; }
		MemberDeclarationSyntax Generate();
	}

	public abstract class TypeBase
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public bool IsPublic { get; set; }

		public abstract MemberDeclarationSyntax Generate();
	}
	public class Class
	{
		public string Name { get; set; }
		public bool IsPublic { get; set; }
		public List<string> BaseTypeList { get; set; }
		public List<string> AttributeList { get; set; }
		public List<Field> FieldList { get; set; }
		public List<Property> PropertyList { get; set; }
		public List<Method> MethodList { get; set; }
		private ClassDeclarationSyntax @class;
		private NamespaceDeclarationSyntax @namespace;

		public Class()
		{
			BaseTypeList = new List<string>();
			AttributeList = new List<string>();
		}
		public Class(string _className, bool _isPublic = true) : this()
		{
			Name = _className;
			IsPublic = _isPublic;
		}
		public Class AddBaseTypes(params string[] _baseTypes)
		{
			BaseTypeList.AddRange(_baseTypes);
			return this;
		}
		public Class AddAttributes(params string[] _attributes)
		{
			AttributeList.AddRange(_attributes);
			return this;
		}

		public ClassDeclarationSyntax Generate()
		{
			@class = SyntaxFactory.ClassDeclaration("Order");
			if (IsPublic) @class = @class.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
			@class = @class.AddBaseListTypes(BaseTypeList.Select(c => SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(c))).ToArray());
			foreach (var att in AttributeList)
				@class = @class.AddAttributeLists(SyntaxFactory.AttributeList(
					SyntaxFactory.SeparatedList(new List<AttributeSyntax>() {
					SyntaxFactory.Attribute(SyntaxFactory.ParseName(att))})));

			// Add the field, the property and method to the class.
			@class = @class.AddMembers(FieldList.Select(c => c.Generate()).Cast<MemberDeclarationSyntax>()
				.Concat(PropertyList.Select(c => c.Generate()).Cast<MemberDeclarationSyntax>())
				.Concat(MethodList.Select(c => c.Generate()).Cast<MemberDeclarationSyntax>()).ToArray());

			return @class;
		}
	}
}
