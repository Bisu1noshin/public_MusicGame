#pragma once
#include "GL/glew.h"
#pragma comment(lib, "opengl32.lib")

class DynamicVertexArray {
public:
	DynamicVertexArray() : numVerts(0), numIndices(0), vertexBuffer(0), indexBuffer(0), vertexArray(0), savedvBuff{} {} //宣言のためだけに実装　基本的には使わないこと
	DynamicVertexArray(const float* verts, unsigned int numVerts_, const unsigned int* indices, unsigned int numIndices_);
	~DynamicVertexArray();
	void SetActive() const;
	void ChangeTexCoord(float* f);
	bool HaveToChange(float* f);
	void SetsavedBuff(float* f);
private:

	unsigned int numVerts, numIndices;
	unsigned int vertexBuffer, indexBuffer;
	unsigned int vertexArray;
	float savedvBuff[8];
};
