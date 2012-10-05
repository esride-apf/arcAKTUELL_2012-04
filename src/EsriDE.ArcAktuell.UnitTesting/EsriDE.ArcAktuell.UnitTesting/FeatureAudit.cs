using System;
using ESRI.ArcGIS.Geodatabase;

namespace EsriDE.ArcAktuell.UnitTesting
{
	public static class FeatureAudit
	{
		private const string _fieldName = "Timestamp";

		public static void UpdateTimestamp(IFeature feature)
		{
			var field = feature.Fields.FindField(_fieldName);
			var timestamp = DateTime.Now;
			feature.set_Value(field, timestamp);
		}
	}
}