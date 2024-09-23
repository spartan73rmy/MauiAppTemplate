namespace Season.DesarrolloBVF.Domain.Responses
{
    public class GetImagesQueryResponse
    {
        public IEnumerable<byte[]> Images { get; }

        public GetImagesQueryResponse(IEnumerable<byte[]> images)
        {
            this.Images = images;
        }
    }
}
