using ESRI.ArcGIS.Geodatabase;
using Moq;
using NUnit.Framework;

namespace EsriDE.ArcAktuell.UnitTesting.Tests
{
	[TestFixture]
	public class FeatureAuditFixture
	{
		[Test]
		public void test_set_of_timestamp_for_feature()
		{
			var feature = new Mock<IFeature>().Object;
			FeatureAudit.UpdateTimestamp(feature);
		}
	}
}
