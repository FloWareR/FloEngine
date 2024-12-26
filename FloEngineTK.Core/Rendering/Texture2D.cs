using OpenTK.Graphics.OpenGL4;

namespace FloEngineTK.Core.Rendering
{
    public class Texture2D : IDisposable
    {
        private bool _disposed;
        public int Handle { get; private set; }
        public Texture2D(int handle) 
        {
            Handle = handle;
        }

        ~Texture2D()
        {
            Dispose(false);
        }

        public void Use()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }


        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                GL.DeleteTexture(Handle);
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
