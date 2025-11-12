#pragma once
#define _CRT_SECURE_NO_WARNINGS

#include <fstream>
#include "System/Actor.h"
#include <sstream>
#include "fmod.hpp"
#include "fmod_errors.h"
#include "NotesManager.h"





class FManager : public Actor {
public:
	FManager(Game* owner);
	~FManager();
	void UpdateActor() override;
	void Render2D() override;

private:
	void BackTime(unsigned int miliSec);
	void ForwardTime(unsigned int miliSec);

	void FMOD_Init();
	void FMOD_Check(FMOD_RESULT result);
	void InputNotes(const std::string& string);

	FMOD::Sound* sound;
	FMOD::System* system;
	FMOD::Channel* channel;
	bool stopping;

	float time;

	int bpm; //4分音符が1分間に入る個数
	std::string musicName = "./data/maou_14_shining_star.mp3"; //使う音楽のファイル名を入れる
	const std::string filename = "./data/savedNotes.txt";

	NotesManager* mNotesManager;
};