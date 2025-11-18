#pragma once
#include "System/Actor.h"
#include <algorithm>
#include <vector>

enum class Direction : short {
	Non = -1, Top, Right, Down, Left
};

enum class MouseOperation : short {
	Non, LeftOn, RightOn, RightHold
};
enum class NotesType : short {
	Flick, Hold, Rush
};

class Notes {
public:
	Notes(class NotesManager* owner_) : owner(owner_), time(0), dir(Direction::Non), type(NotesType::Flick), lane(0) {}
	~Notes();

	NotesManager* owner;
	int time; //判定が出現する時間　単位はノーツラインの本数
	Direction dir; //入力の方向
	NotesType type; //HOLDノーツか否か
	int lane; //第1レーンか第2レーンか
};
class NotesManager : public Actor {
public:
	NotesManager(Game* game);
	~NotesManager();
	void AddNotes(Notes* notes);
	void RemoveNotes(Notes* notes);
	void CreateNotes(int time_, int dirN, int typeN, int lane_);
	void ClearNotes() { mNotes.clear(); }
	const std::string CreateNotesStr();

	//FManagerから呼び出す同期用関数群
	void SetTime(float time_) { time = time_; }
	void SetBpm(int bpm_) { bpm = bpm_; }
	void SetLength(float length_) { length = length_; }
	void SetStopping(bool s_) { stopping = s_; }

	void DeleteAllNotes();

protected:
	void ActorInput(const InputState& kState) override;
	void UpdateActor() override;
	void Render2D() override;
	
private:


	std::vector<Notes*> mNotes;
	SpriteComponent* notes_img;
	SpriteComponent* bpm_line;
	SpriteComponent* red_box;
	SpriteComponent* green_box;

	float bpm;
	float time;
	float leftTime;
	float length; //曲の長さ(秒)
	bool stopping;
	MouseOperation mouse_button;
	bool notes_erase;
	bool enter_on;
	bool rush_mode;

	Rect selectingBox;

	Direction lastInput;
	Vector2 mousePos;

	const int NOTES_SIZE = 32;
	const int shosetsu_num = 4; //小節線を何本ごとに入れるか
	const Color little_clear_color = Color(1.f, 1.f, 1.f, .25f);
};