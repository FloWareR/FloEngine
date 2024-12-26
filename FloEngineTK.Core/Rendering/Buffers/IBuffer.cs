namespace FloEngineTK.Core.Rendering
{
    public interface IBuffer
    {
        int BufferID { get; }
        void Bind();
        void Unbind();
    }
}
