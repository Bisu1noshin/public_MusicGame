#include "DynamicVertexArray.h"

DynamicVertexArray::DynamicVertexArray(const float* verts, unsigned int numVerts_, const unsigned int* indices, unsigned int numIndices_) : numVerts(numVerts_), numIndices(numIndices_), savedvBuff{} {
	glGenVertexArrays(1, &vertexArray);
	glBindVertexArray(vertexArray);

	glGenBuffers(1, &vertexBuffer);
	glBindBuffer(GL_ARRAY_BUFFER, vertexBuffer);
	glBufferData(GL_ARRAY_BUFFER, sizeof(float) * numVerts * 5 , verts, GL_DYNAMIC_DRAW);

	glGenBuffers(1, &indexBuffer);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, indexBuffer);

	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(unsigned int) * numIndices, indices, GL_DYNAMIC_DRAW);

	glEnableVertexAttribArray(0);
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, sizeof(float) * 5, 0);
	glEnableVertexAttribArray(1);
	glVertexAttribPointer(1, 2, GL_FLOAT, GL_FALSE, sizeof(float) * 5, reinterpret_cast<void*>(sizeof(float) * 3));

	glBindVertexArray(0);
}
DynamicVertexArray::~DynamicVertexArray() {
	glDeleteBuffers(1, &vertexBuffer);
	glDeleteBuffers(1, &indexBuffer);
	glDeleteVertexArrays(1, &vertexArray);
}
void DynamicVertexArray::SetActive() const {
	if (this == nullptr) return;
	glBindVertexArray(vertexArray);
}

void DynamicVertexArray::ChangeTexCoord(float* f) {
	for (unsigned int i = 0; i < 4; ++i) {
		glBindBuffer(GL_ARRAY_BUFFER, vertexBuffer);
		glBufferSubData(GL_ARRAY_BUFFER,
			sizeof(float) * i * 5  + 3 * sizeof(float), sizeof(float) * 2, f + i * 2);
	}
}

bool DynamicVertexArray::HaveToChange(float* f) {
	bool r = false;
	for (int i = 0; i < 8; ++i) {
		if (savedvBuff[i] != *(f + i)) {
			r = true;
		}
	}
	//’l‚ªˆá‚Á‚Ä‚¢‚½‚çXV
	if (r) {
		SetsavedBuff(f);
	}
	return r;
}
void DynamicVertexArray::SetsavedBuff(float* f) {
	for (int i = 0; i < 8; ++i) {
		savedvBuff[i] = *(f + i);
	}
}