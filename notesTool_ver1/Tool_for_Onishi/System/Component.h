#pragma once
#include "Math.h"
#include "Texture.h"
#include "DynamicVertexArray.h"


class Component {
public:
	Component(class Actor* a);
	virtual ~Component();

	virtual void Update() {}
	virtual void OnUpdateWorldTransform() {}
	virtual void ProcessInput(const class InputState& state) {}

	class Actor* GetOwner() const { return owner; }
	class Game* GetGame() const;

	template <class T> bool Equals();

protected:
	class Actor* owner;
	class Game* game;
};

class Transform2 : public Component { //2ŽŸŒ³À•W‹óŠÔ“à‚Ìî•ñ
public:
	Vector2 position;
	Quaternion rotation;
	Vector2 scale;
	Vector2 center;
	Transform2(Actor* owner) : Component(owner), position(), rotation(), scale(1, 1), center() {}
	~Transform2() { }
};

class Transform3 : public Component { //3ŽŸŒ³À•W‹óŠÔ“à‚Ìî•ñ
public:
	Vector3 scale, center;
	Vector2 position;
	Quaternion rotation;
	Transform3(Actor* owner) : Component(owner), position(), rotation(), scale(1, 1, 1), center() {}
	~Transform3() { }

};

constexpr unsigned int VERTICES_LENGTH = 4;
constexpr unsigned int INDICES_LENGTH = 6;

class SpriteComponent : public Component {
public:
	SpriteComponent(Actor* owner, int draworder = 100);
	~SpriteComponent();
	void Draw();
	void Draw(const Rect& draw, const Rect& src);
	void Draw(const Rect& draw, const Rect& src, const Color& color);
	int GetDrawOrder() const { return drawOrder; }
	int GetTexHeight() const { return texHeight; }
	int GetTexWidth() const { return texWidth; }
	bool LoadTexture(const std::string& filename) { return tex->Load(filename); }

private:
	Texture* tex;
	int drawOrder;
	int texWidth, texHeight;
	DynamicVertexArray* dVertex;
};