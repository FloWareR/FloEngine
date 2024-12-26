using FloEngineTK.Core.Rendering;

namespace FloEngineTK.Core.Management
{
    public class ResourceManager
    {
        private static ResourceManager? instance = null;
        private static readonly object _loc = new();
        private IDictionary<string, Texture2D> _textureCache = new Dictionary<string, Texture2D>();

        public static ResourceManager Instance {
            get { 
                lock (_loc) { 
                    instance ??= new ResourceManager();
                    return instance;
                }
            }
        }

        public Texture2D LoadTexture(string texturePath)
        {
            _textureCache.TryGetValue(texturePath, out var value);
            if (value is not null){
                return value;
            }
            value = TextureFactory.Load(texturePath);
            _textureCache.Add(texturePath, value);
            return value;
        }
    }
}
