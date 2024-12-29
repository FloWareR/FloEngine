using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using FloEngineTK.Core.Rendering;

public class DebugRenderer
{
    private readonly Shader _shader;

    public DebugRenderer(Shader shader)
    {
        _shader = shader;
    }

    public void DrawRectangle(Vector3 min, Vector3 max, Matrix4 projection, Matrix4 transform)
    {
        // Define rectangle vertices with green color (0, 1, 0) and default texture index (0)
        float[] vertices = {
            // Position         // TexCoord  // Color        // Texture Index
            min.X, min.Y, 0f,   0f, 0f,      0f, 1f, 0f,     0f,  // Bottom left
            max.X, min.Y, 0f,   1f, 0f,      0f, 1f, 0f,     0f,  // Bottom right
            max.X, max.Y, 0f,   1f, 1f,      0f, 1f, 0f,     0f,  // Top right
            min.X, max.Y, 0f,   0f, 1f,      0f, 1f, 0f,     0f   // Top left
        };

        // Define indices for line loop (edges)
        uint[] indices = {
            0, 1, 1, 2, 2, 3, 3, 0
        };

        // Set up OpenGL buffers
        int vao = GL.GenVertexArray();
        GL.BindVertexArray(vao);

        int vbo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);

        int ebo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.DynamicDraw);

        // Enable vertex attributes
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 9 * sizeof(float), 0);                     // Position
        GL.EnableVertexAttribArray(0);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 9 * sizeof(float), 3 * sizeof(float));     // TexCoord
        GL.EnableVertexAttribArray(1);

        GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 9 * sizeof(float), 5 * sizeof(float));     // Color
        GL.EnableVertexAttribArray(2);

        GL.VertexAttribPointer(3, 1, VertexAttribPointerType.Float, false, 9 * sizeof(float), 8 * sizeof(float));     // Texture Index
        GL.EnableVertexAttribArray(3);

        // Use the shader
        _shader.Use();

        // Set the uniform for the projection matrix
        _shader.SetMatrix4("projection", projection);
        _shader.SetMatrix4("transform", transform);

        // Draw the rectangle (as line loop for debug visualization)
        GL.BindVertexArray(vao);
        GL.DrawElements(PrimitiveType.Lines, indices.Length, DrawElementsType.UnsignedInt, 0);

        // Clean up
        GL.DeleteBuffer(vbo);
        GL.DeleteBuffer(ebo);
        GL.DeleteVertexArray(vao);
    }
}
