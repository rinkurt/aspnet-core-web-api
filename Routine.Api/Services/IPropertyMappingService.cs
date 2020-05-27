using System.Collections.Generic;

namespace Routine.Api.Services
{
	public interface IPropertyMappingService
	{
		Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSrc, TDest> ();
	}
}