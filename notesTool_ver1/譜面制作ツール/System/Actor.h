#pragma once
#include "Coordinate.h"
#include <string>
#include <vector>
#include "Component.h"
#include "InputSystem.h"
#include "Actor.h"
#include "Game.h"

/// <summary>
/// ゲーム上で動かすアクターの基底クラス
/// このままでもインスタンス化できるが、後片付けが大変なので禁止
/// </summary>

const int TILE_SIZE = 96;

class Actor {
public:
	enum State { Active, Pause, Dead };
	Actor(Game* game);
	virtual ~Actor();

	State GetState() const { return state; }
	Game* GetOwner() const { return owner; }
	template <class T> T* GetComponent() const;
	std::string GetName() const { return name; }
	const mat4 GetWorldTransform() const;
	Vector3 GetForward() const {
		return Vector3::Transform(Vector3::UnitX, transform.rotation);
	}

	void SetState(State s) { state = s; }
	void SetName(std::string name) { this->name = name; }
	void AddComponent(Component* c);
	void RemoveComponent(Component* c);
	template <class T> T* CreateComponent() { return new T(this); }

	void ProcessInput(const InputState& kstate);
	//void ComputeWorldTransform();
	void Update();
	void RenderActor();

	Transform3 transform;

protected:
	virtual void ActorInput(const InputState& kState) {}
	virtual void UpdateActor() {}
	virtual void Render2D() {}

	State state;
	std::vector<Component*> Components;
	std::string name;
	Game* owner;

	mat4 worldTransform;
	//bool recomputeWorldTransform;
private:
	void UpdateComponents();
	bool alreadyRendered;
};