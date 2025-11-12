#include "Game.h"
#include "Component.h"
#include "Actor.h"
#include "Math.h"

Component::Component(Actor* a) : owner(a), game(a->GetOwner()) {
	owner->AddComponent(this);
}

Component::~Component() {
	owner->RemoveComponent(this);
}

Game* Component::GetGame() const { return owner->GetOwner(); }

template <class T> bool Component::Equals() {
	T* t = static_cast<T>(this);
	return t != nullptr;
}


SpriteComponent::SpriteComponent(Actor* owner, int draworder) : Component(owner), tex(), drawOrder(draworder), texWidth(1), texHeight(1) {
	owner->GetOwner()->AddSprite(this);
	tex = new Texture;

	//DVAの初期化
	float vertices[] = {
		0.f, 1.f, 0.f, 0.f, 0.f,
		1.f, 1.f, 0.f, 1.f, 0.f,
		1.f, 0.f, 0.f, 1.f, 1.f,
		0.f, 0.f, 0.f, 0.f, 1.f
	};
	unsigned int indices[] = {
		0, 1, 2, 2, 3, 0
	};
	float uvBuff[] = {
		0.f, 0.f,
		1.f, 0.f,
		1.f, 1.f,
		0.f, 1.f
	};
	dVertex = new DynamicVertexArray(vertices, VERTICES_LENGTH, indices, INDICES_LENGTH);
	dVertex->SetsavedBuff(uvBuff);
}
SpriteComponent::~SpriteComponent() {
	delete tex;
	GetGame()->RemoveSprite(this);
}
void SpriteComponent::Draw() { //もう使わない
	mat4 scale = mat4::CreateMatrixfor(static_cast<float>(tex->GetWidth()), static_cast<float>(tex->GetHeight()));
	mat4 world = scale * owner->GetWorldTransform();
	owner->GetOwner()->GetShader()->SetMatrixUniform("WorldTransform", world);
	tex->SetActive();
	glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, nullptr);
}

void SpriteComponent::Draw(const Rect& draw, const Rect& src) {
	Draw(draw, src, Color(1, 1, 1, 1));
}

void SpriteComponent::Draw(const Rect& draw, const Rect& src, const Color& color) {
	//スクリーン座標　　　ソースの座標　　　　色、透明度

	//テクスチャ座標を計算
	float sx = (float)src.x / tex->GetWidth();
	float sy = (float)src.y / tex->GetHeight();
	float ex = (float)(src.x + src.w) / tex->GetWidth();
	float ey = (float)(src.y + src.h) / tex->GetHeight();
	float currVerts[]{ sx, sy, ex, sy, ex, ey, sx, ey };

	if (dVertex->HaveToChange(currVerts)) {
		dVertex->ChangeTexCoord(currVerts);
	}

	mat4 scale = mat4::CreateMatrixfor(draw.w, draw.h);
	scale.Translation(draw.x, draw.y);
	owner->GetOwner()->GetShader()->SetMatrixUniform("WorldTransform", scale);
	owner->GetOwner()->GetShader()->SetColor(color);

	dVertex->SetActive();

	tex->SetActive();
	glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, nullptr);
}