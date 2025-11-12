#pragma once
#include "fmod.hpp"
#include "fmod_errors.h"
#include "fmod_studio.hpp"

class AudioSystem {
public:
	AudioSystem(class Game* game);
	~AudioSystem();

	bool Initialize();
	void Shutdown();
	void Update(float deltaTime);
private:
	class Game* game;
	FMOD::Studio::System* mSystem;
	FMOD::System* lowLevelSystem;
};