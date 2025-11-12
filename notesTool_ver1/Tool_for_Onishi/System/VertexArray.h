#pragma once
#include "GL/glew.h"
#pragma comment(lib, "opengl32.lib")

class VertexArray {
public:
	VertexArray() : numVerts(0), numIndices(0), vertexBuffer(0), indexBuffer(0), vertexArray(0) {} //宣言のためだけに実装　基本的には使わないこと
	VertexArray(const float* verts, unsigned int numVerts_, const unsigned int* indices, unsigned int numIndices_);
	~VertexArray();
	void SetActive() const;
private:
	unsigned int numVerts, numIndices;
	unsigned int vertexBuffer, indexBuffer;
	unsigned int vertexArray;
};