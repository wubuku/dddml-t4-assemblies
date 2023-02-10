//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dddml.T4.Templates.Java
{
    using Dddml.Core.Dom;
    using System.Collections.Generic;
    using System.Xml;
    using Dddml.T4.Extensions;
    using Dddml.T4.Extensions.Java;
    using Dddml.Ofbiz;
    using Dddml.Ofbiz.Processors;
    using System.IO;
    using Dddml.Serialization;
    using Dddml.T4;
    using Dddml.T4.Extensions.Hibernate;
    using T4Toolbox;
    using System;


    public partial class GenerateStatusItemOfbizDataConstObjects : global::Microsoft.VisualStudio.TextTemplating.TextTransformation
    {

        private global::Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost hostValue;


#line 10 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"


        public class OfbizDataConstObjectTemplate : CSharpTemplate
        {

            public string DataPath { get; set; }

            public string FileFilter { get; set; }

            public string EntityName { get; set; }

            public string ConstObjectName { get; set; }

            public BoundedContext BoundedContext { get; set; }

            private string _descriptionAttributeName = "description";

            public string DescriptionAttributeName
            {
                get { return _descriptionAttributeName; }
                set { _descriptionAttributeName = value; }
            }

            private string _idValueAttributeName;

            public string IdValueAttributeName
            {
                get
                {
                    if (_idValueAttributeName != null)
                    {
                        return _idValueAttributeName;
                    }
                    else
                    {
                        return BoundedContext.AllEntities[this.EntityName].Id.CamelCaseName();
                    }
                }
                set
                { _idValueAttributeName = value; }
            }

            public string IdNameAttributeName { get; set; }

            public bool IsFieldNameUpperCase { get; set; }

            public OfbizDataConstObjectTemplate(BoundedContext boundedContext,
                string dataPath, string fileFilter, string entityName, string constObjName)
                    : this(boundedContext, dataPath, fileFilter, entityName, constObjName, null)
            {
            }

            public OfbizDataConstObjectTemplate(BoundedContext boundedContext,
                string dataPath, string fileFilter, string entityName, string constObjName, string idValAttrName)
                    : this(boundedContext, dataPath, fileFilter, entityName, constObjName, idValAttrName, null)
            {
            }

            public OfbizDataConstObjectTemplate(BoundedContext boundedContext,
                string dataPath, string fileFilter, string entityName, string constObjName, string idValAttrName, string idNameAttrName)
            {
                this.BoundedContext = boundedContext;
                this.DataPath = dataPath;
                this.FileFilter = fileFilter;
                this.EntityName = entityName;
                this.ConstObjectName = constObjName;
                this.IdValueAttributeName = idValAttrName;
                this.IdNameAttributeName = idNameAttrName;
            }

            public override string TransformText()
            {
                base.TransformText();

                string constObjectPackage = BoundedContext.AllEntities[this.EntityName].Aggregate.GetDomainAggregatePackage();

                //var constObjectUsingNamespaces = ConstObject.Aggregate.GetUsingNamespaces();

#line default
#line hidden


#line 88 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                this.Write("package ");

#line default
#line hidden


#line 88 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                this.Write(global::Microsoft.VisualStudio.TextTemplating.ToStringHelper.ToStringWithCulture(constObjectPackage));

#line default
#line hidden


#line 88 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                this.Write(";\n");

#line default
#line hidden


#line 89 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                //foreach (var ns in constObjectUsingNamespaces)
                //{
                //using #= ns #;
                //}

#line default
#line hidden


#line 95 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                this.Write("\n");

#line default
#line hidden


#line 96 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                var className = this.ConstObjectName;
                ISet<string> allIds = new HashSet<string>();

#line default
#line hidden


#line 100 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                this.Write("\npublic class ");

#line default
#line hidden


#line 101 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                this.Write(global::Microsoft.VisualStudio.TextTemplating.ToStringHelper.ToStringWithCulture(className));

#line default
#line hidden


#line 101 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                this.Write(" {\n");

#line default
#line hidden


#line 102 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                foreach (var cv in EntityDataProcessor.GetEntityXmlElements(this.DataPath, this.FileFilter, this.EntityName))
                {
                    var idVal = cv.Attributes[this.IdValueAttributeName].Value;
                    string idFieldName = idVal;//SimpleStringUtils.ToUnderscoredUpperCase(idVal);
                    if (!String.IsNullOrWhiteSpace(this.IdNameAttributeName) && cv.Attributes[this.IdNameAttributeName] != null)
                    {
                        idFieldName = cv.Attributes[this.IdNameAttributeName].Value;
                    }
                    if (IsFieldNameUpperCase)
                    {
                        idFieldName = idFieldName.ToUpperInvariant();
                    }
                    if (JavaUtils.JavaKeywords.Contains(idFieldName))
                    {
                        idFieldName = "_" + idFieldName;
                    }

                    idFieldName = idFieldName
                        .Replace('\\', '_')
                        .Replace('/', '_')
                        .Replace('.', '_')
                        .Replace('-', '_');

                    // /////////////////////////////////////////
                    if (allIds.Contains(idFieldName)) { continue; } else { allIds.Add(idFieldName); }
                    // /////////////////////////////////////////

                    string description = cv.Attributes[this.DescriptionAttributeName] != null ? cv.Attributes[this.DescriptionAttributeName].Value : idFieldName;
                    if (!description.EndsWith(".")) { description = description + "."; }

#line default
#line hidden


#line 133 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                    this.Write("    /**\n     * ");

#line default
#line hidden


#line 134 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                    this.Write(global::Microsoft.VisualStudio.TextTemplating.ToStringHelper.ToStringWithCulture(description));

#line default
#line hidden


#line 134 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                    this.Write("\n     */\n    public static final String ");

#line default
#line hidden


#line 136 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                    this.Write(global::Microsoft.VisualStudio.TextTemplating.ToStringHelper.ToStringWithCulture(idFieldName));

#line default
#line hidden


#line 136 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                    this.Write(" = \"");

#line default
#line hidden


#line 136 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                    this.Write(global::Microsoft.VisualStudio.TextTemplating.ToStringHelper.ToStringWithCulture(idVal));

#line default
#line hidden


#line 136 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                    this.Write("\";\n\n");

#line default
#line hidden


#line 138 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                }

#line default
#line hidden


#line 141 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                this.Write("}\n\n");

#line default
#line hidden


#line 143 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/Domain/Ofbiz/OfbizDataConstObjectTemplate.tt"

                return this.GenerationEnvironment.ToString();
            }
        }

#line default
#line hidden


#line 33 "/Users/yangjiefeng/wms/java/Dddml.Wms.JavaCommon/src/generated/java/org/dddml/wms/LoadBoundedContext.tt"


        private BoundedContext LoadBoundedContextFiles()
        {
            string projectFile = TransformationContext.Current.GetPropertyValue("MSBuildProjectFullPath");
            string projectDir = System.IO.Path.GetDirectoryName(projectFile);

            //var excludedFiles = new List<string>();
            //excludedFiles.Add("Audience.yaml");
            //excludedFiles.Add("IdentityManagement.yaml");
            //excludedFiles.Add("AccessManagement.yaml");
            //
            //string filePath1 = System.IO.Path.Combine(projectDir, "../../Dddml.Wms.Metadata/AttributeSetInstanceDddml.yaml");
            //string filePath2 = System.IO.Path.Combine(projectDir, "../../Dddml.Wms.Metadata/AttributeSetInstanceExtensionFieldGroupDddml.yaml");
            //var additionalFiles = new List<string>();
            //additionalFiles.Add(filePath1);
            //additionalFiles.Add(filePath2);
            //
            //var dddmlDir = System.IO.Path.Combine(projectDir, "..\\..\\dddml");
            //
            //BoundedContext boundedContext = BoundedContextUtils.LoadFromDirectory(dddmlDir, "*.yaml", excludedFiles, additionalFiles);

            BoundedContext boundedContext = BoundedContextUtils.LoadFromProjectFile(System.IO.Path.Combine(projectDir, "..\\..\\dddml\\wms.project"));

            return boundedContext;
        }

#line default
#line hidden


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                T4Toolbox.TransformationContext.Cleanup();

            }
        }


        public global::Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost Host
        {
            get
            {
                return this.hostValue;
            }
            set
            {
                this.hostValue = value;
            }
        }

        public override string TransformText()
        {
            this.GenerationEnvironment = null;

#line 5 "/Users/yangjiefeng/GitHub/dddml-dotnet-tools/Dddml.T4.Templates.Java/DddmlOfbiz.tc"



#line default
#line hidden

#line 13 "/Users/yangjiefeng/wms/java/Dddml.Wms.JavaCommon/src/generated/java/org/dddml/wms/LoadBoundedContext.tt"

            // 
            // include file="$(SolutionDir)Dddml.Wms.Common\LoadBoundedContextFiles.tt" 
            //

            // ///////////////////////////////////////////
            var boundedContext = LoadBoundedContextFiles();
            boundedContext.Refresh();
            boundedContext.ReplacePropertyTypesWithBaseTypes();

            // ///////////////////////////////////////////
            var generationOptions = new GenerationOptions();
            generationOptions.Java = true;
            generationOptions.NoMViewObjects = true;
            boundedContext.PrepareGeneration(generationOptions);

            var mviewObjects = boundedContext.GetMViewObjects();




#line default
#line hidden

#line 6 "/Users/yangjiefeng/wms/java/Dddml.Wms.JavaCommon/src/generated/java/org/dddml/wms/domain/statusitem/GenerateStatusItemOfbizDataConstObjects.tt"

            string projectFile = TransformationContext.Current.GetPropertyValue("MSBuildProjectFullPath");
            string projectDir = System.IO.Path.GetDirectoryName(projectFile);

            string filePath1 = System.IO.Path.Combine(projectDir, "..\\..\\Data");

            var template1 = new OfbizDataConstObjectTemplate(boundedContext,
                filePath1, // data files path
                "*Data.xml", // data file filter
                "StatusItem", // entity name
                "StatusItemIds"//, // generated class name(file name)
                               //"statusId" // the xml attribute name of Id.
                );
            template1.Output.Encoding = new System.Text.UTF8Encoding(false);
            template1.RenderToFile("StatusItemIds.java");



#line default
#line hidden
            return this.GenerationEnvironment.ToString();
        }

        public override void Initialize()
        {
            T4Toolbox.TransformationContext.Initialize(this, this.GenerationEnvironment);

            base.Initialize();

        }
    }
}
