using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;

namespace EsriDE.ArcAktuell.UnitTesting
{
	public class ObjectsFilter
	{
		internal void FilterUnlinkedObjects(string objectClass, IList<string> erpIds)
		{
			var featureClass = GetFeatureClass(objectClass);
			_logger.LogMessage(ServerLogger.msgType.infoDetailed, SoeName, 0, "...");
			var filter = GetQueryFilter(erpIds);
			var indexErpId = featureClass.FindField("SAP_ID");
			var cursor = featureClass.Search(filter, false);
			IFeature feature;
			while (null != (feature = cursor.NextFeature()))
			{
				var dbValue = feature.Value[indexErpId];
				if (dbValue == DBNull.Value)
					continue;
				var erpId = (string)dbValue;
				if (!string.IsNullOrEmpty(erpId) && erpIds.Contains(erpId))
					erpIds.Remove(erpId);
			}
		}

		internal static IQueryFilter GetQueryFilter(IList<string> erpIds)
		{
			IQueryFilter filter = new QueryFilter();
			filter.WhereClause = FilterUtil.GetWhereClauseWithIn("SAP_ID", erpIds, true);
			return filter;
		}

		// unterhalb kommt nur noch Fake-Code um die Methode "grün" zu machen
		private readonly ServerLogger _logger;
		private object SoeName;

		internal IFeatureClass GetFeatureClass(string featureClassName)
		{
			return null;
		}
	}

	public class ServerLogger
	{
		public void LogMessage(object infoDetailed, object soeName, int i, string s)
		{
			throw new NotImplementedException();
		}

		internal enum msgType
		{
			infoDetailed
		}
	}

	public class FilterUtil
	{
		public static string GetWhereClauseWithIn(string sapId, IList<string> erpIds, bool b)
		{
			throw new NotImplementedException();
		}
	}
}