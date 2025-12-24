#include "AudioSystem.h"
#include <iostream>

AudioSystem::AudioSystem(Game* game) : game(game) {
	if (!Initialize()) {
		std::cout << "Error! Failed to init AudioSystem\n";
	}
}

bool AudioSystem::Initialize() {
	FMOD::Debug_Initialize(FMOD_DEBUG_LEVEL_ERROR, FMOD_DEBUG_MODE_TTY);
	FMOD_RESULT result;
	result = FMOD::Studio::System::create(&mSystem);
	if (result != FMOD_OK) {
		std::cout << "Error! Failed to create FMOD system : " << FMOD_ErrorString(result) << std::endl;
		return false;
	}
	result = mSystem->initialize(512, FMOD_STUDIO_INIT_NORMAL, FMOD_INIT_NORMAL, nullptr);
	if (result != FMOD_OK) {
		std::cout << "Error! Failed to init FMOD system : " << FMOD_ErrorString(result) << std::endl;
		return false;
	}
	mSystem->getCoreSystem(&lowLevelSystem);
	return true;
}