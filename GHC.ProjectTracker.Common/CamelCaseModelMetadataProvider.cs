using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GHC.ProjectTracker.Common
{
    public class CamelCaseModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        // Uppercase followed by lowercase but not on existing word boundary (eg. the start)
        Regex _camelCaseRegex = new Regex(@"\B\p{Lu}\p{Ll}", RegexOptions.Compiled);

        // Creates a DisplayName from the model’s property name if one hasn't been specified
        protected override ModelMetadata GetMetadataForProperty(
           Func<object> modelAccessor,
           Type containerType,
           PropertyDescriptor propertyDescriptor)
        {
            ModelMetadata metadata = base.GetMetadataForProperty(modelAccessor, containerType, propertyDescriptor);

            if (metadata.DisplayName == null)
                metadata.DisplayName = displayNameFromCamelCase(metadata.GetDisplayName());

            return metadata;
        }

        string displayNameFromCamelCase(string name)
        {
            name = _camelCaseRegex.Replace(name, " $0");
            if (name.EndsWith(" Id"))
                name = name.Substring(0, name.Length - 3);
            return name;
        }

    }
}
