#include "Shader.h"
#include "SDL3/SDL.h"

bool Shader::ReadShaderSource(GLuint shaderObj, std::string fileName) {
	std::ifstream ifs(fileName);
	if (!ifs) {
		std::cerr << "Error: invalid fileName" << std::endl;
		return true;
	}
	std::string source, line;
	while (getline(ifs, line)) {
		source += line + "\n";
	}
	const GLchar* sourcePtr = (const GLchar*)source.c_str();
	GLint length = (GLuint)source.length();
	glShaderSource(shaderObj, 1, &sourcePtr, &length);

	return false;
}
bool Shader::MakeShader(const std::string& vertexFileName, const std::string& fragmentFileName) {
	GLuint vertShader, fragShader;
	if (!CompileShader(vertexFileName, GL_VERTEX_SHADER, vertShader) || !CompileShader(fragmentFileName, GL_FRAGMENT_SHADER, fragShader)) return false;

	shaderProgram = glCreateProgram();
	glAttachShader(shaderProgram, vertShader);
	glAttachShader(shaderProgram, fragShader);

	glLinkProgram(shaderProgram);
	if (!IsLinked(shaderProgram)) return false;

	return true;
}
bool Shader::CompileShader(const std::string& filename, GLenum Type, GLuint& outShader) {
	GLuint inShader = glCreateShader(Type);
	if (ReadShaderSource(inShader, filename)) return false;

	glCompileShader(inShader);
	if (!IsCompiled(inShader)) return false;

	outShader = inShader;
	return true;
}
bool Shader::IsCompiled(GLint shader) {
	GLint compiled;
	glGetShaderiv(shader, GL_COMPILE_STATUS, &compiled);
	if (compiled == GL_FALSE) {
		char buffer[512];
		memset(buffer, 0, 512);
		glGetShaderInfoLog(shader, 511, nullptr, buffer);
		SDL_Log("Compiled error in Fragment shader :\n%s\n", buffer);
		return false;
	}
	return true;
}
bool Shader::IsLinked(GLint shader) {
	GLint linked;
	glGetProgramiv(shader, GL_LINK_STATUS, &linked);
	if (linked == GL_FALSE) {
		char buffer[512];
		memset(buffer, 0, 512);
		glGetProgramInfoLog(shader, 511, nullptr, buffer);
		SDL_Log("Link error :\n%s\n", buffer);
		return false;
	}
	return true;
}
void Shader::SetActive() const {
	glUseProgram(shaderProgram);
}
void Shader::SetMatrixUniform(const char* name, const mat4& matrix) const {
	GLuint loc = glGetUniformLocation(shaderProgram, name);
	glUniformMatrix4fv(loc, 1, GL_TRUE, matrix.GetAsFloatPtr());
}
void Shader::SetColor(const Color& c) const {
	GLint tintLoc = glGetUniformLocation(shaderProgram, "tintColor");
	glUniform4f(tintLoc, c.r, c.g, c.b, c.a);
}