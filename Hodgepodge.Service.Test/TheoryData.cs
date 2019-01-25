namespace Hodgepodge.Service.Test
{
    public class TheoryData : Xunit.TheoryData<string, string>
    {
        public TheoryData()
        {
            Add("d1.tld/s1/s2/s3", "d1.tld/s1/s2");
            Add("d1.tld/s1/s2/s3/", "d1.tld/s1/s2");
            Add("d1.tld/s1/s2", "d1.tld/s1/s2");
            Add("d1.tld/s1/s2/", "d1.tld/s1/s2");
            Add("d1.tld/s1", "d1.tld");
            Add("d1.tld/s1/", "d1.tld");
            Add("d1.tld", "d1.tld");
            Add("d1.tld/", "d1.tld");

            Add("www.d1.tld/s1/s2/s3", "d1.tld/s1/s2");
            Add("www.d1.tld/s1/s2/s3/", "d1.tld/s1/s2");
            Add("www.d1.tld/s1", "d1.tld");
            Add("www.d1.tld/s1/", "d1.tld");

            Add("x.d1.tld", null);

            Add("D1.TLD/S1/S2/S3", "d1.tld/s1/s2");
            Add("D1.TLD/S1/S2/S3/", "d1.tld/s1/s2");
            Add("d1.tld/s1/s2/s3", "D1.TLD/S1/S2");
            Add("d1.tld/s1/s2/s3/", "D1.TLD/S1/S2");
        }
    }
}
