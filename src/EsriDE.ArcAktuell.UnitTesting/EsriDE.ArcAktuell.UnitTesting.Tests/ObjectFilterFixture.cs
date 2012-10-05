using System;
using System.Collections.Generic;
using System.Diagnostics;
using ESRI.ArcGIS.Geodatabase;
using NUnit.Framework;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace EsriDE.ArcAktuell.UnitTesting.Tests
{
	public class ObjectFilterFixture
	{
		[Test, Isolated]
		public void Do()
		{
			try
			{
				var sut = Isolate.Fake.Instance<ObjectsFilter>(
					Members.CallOriginal, ConstructorWillBe.Ignored, BaseConstructorWillBe.Ignored);

				var fakeLogger = Isolate.Fake.Instance<ServerLogger>(Members.ReturnNulls, ConstructorWillBe.Ignored);
				ObjectState.SetField(sut, "_logger", fakeLogger);

				var fakeFeatureClass = Isolate.Fake.Instance<IFeatureClass>();
				Isolate.WhenCalled(() => fakeFeatureClass.FindField("SAP_ID")).WillReturn(1);

				var fakeQueryFilter = Isolate.Fake.Instance<QueryFilter>();
				Isolate.WhenCalled(() => ObjectsFilter.GetQueryFilter(null)).WillReturn(fakeQueryFilter);

				var fakeFeatureCursor = Isolate.Fake.Instance<IFeatureCursor>();
				Isolate.WhenCalled(() => fakeFeatureClass.Search(null, false)).WillReturn(fakeFeatureCursor);

				var fakeFeature1 = Isolate.Fake.Instance<IFeature>();
				Isolate.WhenCalled(() => fakeFeature1.Value[1]).WillReturn("1001");

				var fakeFeature2 = Isolate.Fake.Instance<IFeature>();
				Isolate.WhenCalled(() => fakeFeature2.Value[1]).WillReturn("1002");

				var fakeFeature3 = Isolate.Fake.Instance<IFeature>();
				Isolate.WhenCalled(() => fakeFeature3.Value[1]).WillReturn("1003");

				Isolate.WhenCalled(() => fakeFeatureCursor.NextFeature()).WillReturn(fakeFeature1);
				Isolate.WhenCalled(() => fakeFeatureCursor.NextFeature()).WillReturn(fakeFeature2);
				Isolate.WhenCalled(() => fakeFeatureCursor.NextFeature()).WillReturn(fakeFeature3);
				Isolate.WhenCalled(() => fakeFeatureCursor.NextFeature()).WillReturn(null);

				Isolate.WhenCalled(() => sut.GetFeatureClass("foo")).WillReturn(fakeFeatureClass);

				var fakeErpIds = new List<string> { "999", "1002", "1004" };

				sut.FilterUnlinkedObjects("objectClass", fakeErpIds);

				Assert.That(fakeErpIds, Is.EquivalentTo(new List<string> { "999", "1004" }));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		} 
	}
}