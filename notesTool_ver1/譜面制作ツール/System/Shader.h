#pragma once
#include <fstream>
#include <string>
#include <iostream>
#include <sstream>
#include <gl/glew.h>
#include "Math.h"

#ifndef MY_SHADER //include確認のため一応実装
#define MY_SHADER
#endif

/// <summary>
/// OpenGLで使うシェーダープログラム
/// </summary>
class Shader {
public:
	Shader() : shaderProgram(0) {}
	~Shader() { glDeleteProgram(shaderProgram); }
	bool MakeShader(const std::string& vertexFileName, const std::string& fragmentFileName);
	void SetActive() const;
	void SetMatrixUniform(const char* name, const mat4& matrix) const;
	void SetColor(const Color& c) const;
private:
	bool ReadShaderSource(GLuint shaderObj, std::string fileName);
	bool CompileShader(const std::string& filename, GLenum Type, GLuint& outShader);
	bool IsCompiled(GLint shader);
	bool IsLinked(GLint shader);
	GLuint shaderProgram;
};