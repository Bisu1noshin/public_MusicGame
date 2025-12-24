#include "Actor.h"
#include "Component.h"

Actor::Actor(Game* game)
	: owner(game), state(Active), name(""), Components(0), transform(this), worldTransform(), alreadyRendered(false) /*recomputeWorldTransform(true)*/ 
{
	owner->AddActor(this);
}

Actor::~Actor() {
	owner->RemoveActor(this);
	/*for (auto comp : Components) {
		delete comp;
	}*/
	while (!Components.empty()) {
		delete Components.back();
	}
}
template <class T> T* Actor::GetComponent() const {
	for (Component* c : Components) {
		if (T* casted = dynamic_cast<T*>(c)) {
			return casted;
		}
	}
	return nullptr;
}
void Actor::ProcessInput(const InputState& kstate) {
	if (state == State::Active)
	{
		for (auto comp : Components) {
			comp->ProcessInput(kstate);
		}
		ActorInput(kstate);
	}
}

void Actor::AddComponent(class Component* c) {
	Components.emplace_back(c);
}
void Actor::RemoveComponent(class Component* c) {
	auto it = std::find(Components.begin(), Components.end(), c);
	if (it != Components.end()) {
		Components.erase(it);
	}
}
void Actor::UpdateComponents() {
	for (auto c : Components) {
		c->Update();
	}
}
void Actor::Update() {
	if (state == Active) {
		//ComputeWorldTransform();
		alreadyRendered = false;
		UpdateComponents();
		UpdateActor();
		//ComputeWorldTransform();
	}
}
//void Actor::ComputeWorldTransform() {
//	if (recomputeWorldTransform) {
//		recomputeWorldTransform = false;
//		worldTransform = mat4::CreateMatrixfor(transform.scale);				//スケール
//		worldTransform *= mat4::CreateMatrixfromQuaternion(transform.rotation);	//回転
//		worldTransform *= mat4::CreateTranslationMatrixfor(transform.position);	//平行移動
//	}
//	for (auto comp : Components) {
//		comp->OnUpdateWorldTransform();
//	}
//}
const mat4 Actor::GetWorldTransform() const {
	return worldTransform;
}
void Actor::RenderActor() {
	if (alreadyRendered) { return; }
	Render2D();
	alreadyRendered = true;
}