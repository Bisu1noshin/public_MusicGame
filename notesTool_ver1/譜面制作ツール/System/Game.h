#pragma once
#include "SDL3/SDL.h"
#include "Shader.h"
#include "VertexArray.h"
#include <vector>
#include <string>
#include "Math.h"
#include "InputSystem.h"

#ifndef SDL_ENABLE_OLD_NAMES
#define SDL_ENABLE_OLD_NAMES
#include "SDL3/SDL_oldnames.h"
#endif

/// <summary>
/// 石田先生紹介
/// raylib オススメライブラリ
/// </summary>

class Camera {
public:
	Camera(Rect r) : visible(r), pos() {}

	Rect visible;
	Vector2 pos;
};

class Game {
public:
	Game();
	bool Init();
	void RunLoop();
	void Shutdown();

	void Initialize();
	void UpdateGame();
	void Finalize();

	//Actorを型と名前から取得する
	//コンパイルエラーが出るため、ここに書くしかなかった
	template <class T> T* GetActor(std::string name) const {
		T* a;
		if (updatingActors) {
			for (auto actor : pendingActors) {
				a = dynamic_cast<T*>(actor);
				if (a != nullptr && a->GetName() == name) {
					return a;
				}
			}
		}
		for (auto actor : actors) {
			a = dynamic_cast<T*>(actor);
			if (a != nullptr && a->GetName() == name) {
				return a;
			}
		}
		return nullptr;
	}
	Rect GetWindowSize() const { return windowSize; }
	mat4 GetViewTransform() const;
	Shader* GetShader() const { return spriteShader; }
	InputSystem* GetKeyBoard() const { return mInputSystem; }
	float GetDeltaTime() const { return deltaTime; }
	SDL_Window* GetWindow() const { return window; }

	template <class T> T* CreateActor() { return new T(this); }
	void AddActor(class Actor* a);
	void RemoveActor(class Actor* a);

	void AddSprite(class SpriteComponent* c);
	void RemoveSprite(class SpriteComponent* c);

	void CreateThread(void* func, int num);
	

	bool updatingActors;
	Camera* camera = new Camera(Rect(-520, -359, 1040, 768));
private:
	void Input();
	void Update();
	void Render();
	
	void UpdateActors();
	GLuint CompileShader(GLenum type, const char* src);
	bool LoadShaders();
	void CreateVertexArray();

	SDL_Window* window;
	Shader* spriteShader;
	Rect windowSize = Rect(0, 0, 960, 540); // x : -480 ~ 480  y : -270 ~ 270
	Uint64 TicksCnt;
	float deltaTime;
	bool isRunning;
	std::vector<class Actor*> actors;
	std::vector<class Actor*> pendingActors;
	std::vector<class SpriteComponent*> sprites;
	InputSystem* mInputSystem;

	SDL_GLContext context;
	VertexArray* spriteVerts;

	std::vector<class UIScreen*> mUIStack;
	const std::vector<class UIScreen*>& GetUIStack();
	void PushUI(class UIScreen* screen);
};