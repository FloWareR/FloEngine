using FloEngineTK.Core.Rendering.Shaders;
using OpenTK.Graphics.OpenGL4;


namespace FloEngineTK.Core.Rendering
{
    public class Shader
    {
        public int ProgramID { get; private set; }
        private ShaderProgramSource _shaderProgramSource { get; }
        public bool Compiled { get; private set; }
        public Shader(ShaderProgramSource shaderProgramSource, bool compile = false)
        {
            _shaderProgramSource = shaderProgramSource;
            if (compile)
            {
                CompileShader();
            }
        }

        public bool CompileShader()
        {
            if (_shaderProgramSource == null)
            {
                Console.WriteLine("Shader Program Source is Null");
                return false;
            }
            if (Compiled)
            {
                Console.WriteLine("Shader is already compiled");
                return false;
            }
            int vertexShaderId = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderId, _shaderProgramSource.VertexShaderSource);
            GL.CompileShader(vertexShaderId);
            GL.GetShader(vertexShaderId, ShaderParameter.CompileStatus, out var vertexShaderCompilationCode);
            if (vertexShaderCompilationCode != (int)All.True)
            {
                Console.WriteLine(GL.GetShaderInfoLog(vertexShaderId));
                return false;
            }

            int fragmentShaderId = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShaderId, _shaderProgramSource.FragmentShaderSource);
            GL.CompileShader(fragmentShaderId);
            GL.GetShader(fragmentShaderId, ShaderParameter.CompileStatus, out var fragmenShaderCompilationCode);
            if (fragmenShaderCompilationCode != (int)All.True)
            {
                Console.WriteLine(GL.GetShaderInfoLog(vertexShaderId));
                return false;
            }

            ProgramID = GL.CreateProgram();
            GL.AttachShader(ProgramID, vertexShaderId);
            GL.AttachShader(ProgramID, fragmentShaderId);
            GL.LinkProgram(ProgramID);

            GL.DetachShader(ProgramID, vertexShaderId);
            GL.DetachShader(ProgramID, fragmentShaderId);

            GL.DeleteShader(vertexShaderId);
            GL.DeleteProgram(fragmentShaderId);
            Compiled = true;
            return true;
        }

        public void Use()
        {
            if (!Compiled)
            {
                Console.WriteLine("Shader has not been compiled");
                return;
            }
            GL.UseProgram(ProgramID);     
        }

        public static ShaderProgramSource ParseShader(string filePath)
        {
            string[] shaderSource = new string[2];
            eShaderType shaderType = eShaderType.NONE;
            var allLines = File.ReadAllLines(filePath);
            for(int i = 0; i < allLines.Length; i++)
            {
                string current = allLines[i];
                if (current.ToLower().Contains("#shader"))
                {
                    if (current.ToLower().Contains("vertex"))
                    {
                        shaderType = eShaderType.VERTEX;
                    }
                    else if (current.ToLower().Contains("fragment"))
                    {
                        shaderType = eShaderType.FRAGMENT;
                    }
                    else
                    {
                        Console.WriteLine("Error, No shader type has been supplied");
                    }
                }
                else
                {
                    shaderSource[(int)shaderType] += current + Environment.NewLine;
                }
            }
            return new ShaderProgramSource(shaderSource[(int)eShaderType.VERTEX], shaderSource[(int)eShaderType.FRAGMENT]);
        }
    }
}
