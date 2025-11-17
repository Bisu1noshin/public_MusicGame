#include "NotesManager.h"
#include <sstream>

NotesManager::NotesManager(Game* game) 
	: Actor(game), bpm(0), time(0), leftTime(0), length(0), stopping(false), 
	lastInput(Direction::Top), selectingBox(), mouse_button(MouseOperation::Non), notes_erase(false), enter_on(false), rush_mode(false)
	
{
	bpm_line = new SpriteComponent(this, 2);
	bpm_line->LoadTexture("./data/bpm_line.png");
	red_box = new SpriteComponent(this, 1);
	green_box = new SpriteComponent(this, 1);
	red_box->LoadTexture("./data/red_box.png");
	green_box->LoadTexture("./data/green_box.png");
	notes_img = new SpriteComponent(this, 4);
	notes_img->LoadTexture("./data/notes_img.png");

	DeleteAllNotes();
}
NotesManager::~NotesManager() {
	delete bpm_line, red_box, green_box, notes_img;
	while (!mNotes.empty()) {
		delete mNotes.back();
	}
}
Notes::~Notes() {
	owner->RemoveNotes(this);
}

void NotesManager::AddNotes(Notes* notes) {
	auto it = mNotes.begin();
	for (auto n : mNotes) {
		if (n->time > notes->time) {
			break;
		}
		it++;
	}
	mNotes.insert(it, notes);
}

void NotesManager::RemoveNotes(Notes* notes) {
	auto it = std::find(mNotes.begin(), mNotes.end(), notes);
	if (it != mNotes.end()) {
		mNotes.erase(it);
	}
}

void NotesManager::CreateNotes(int time_, int dirN, int typeN, int lane_) {
	Notes* notes = new Notes(this);
	notes->time = time_;
	notes->dir = (Direction)dirN;
	notes->type = (NotesType)typeN;
	notes->lane = lane_;
	AddNotes(notes);
}

const std::string NotesManager::CreateNotesStr() {
	std::string str;
	for (auto n : mNotes) { //各ノーツの情報を記録
		std::stringstream ss;
		std::string s;
		ss << n->time << ',' << (int)n->dir << ',' << (int)n->type << ',' << n->lane;
		ss >> s;
		str += s;
		str += "\n";
	}
	return str;
}

void NotesManager::ActorInput(const InputState& kState) {
	//マウスの状態取得
	mousePos = kState.mouse.GetPosition();
	Vector2 wheelPos = kState.mouse.GetScrollWheel();
	if (mousePos.x <= NOTES_SIZE * 4 && mousePos.x >= -NOTES_SIZE * 4) {
		if (kState.mouse.GetButtonState(MouseCode::Right) == Down) {
			mouse_button = MouseOperation::RightOn;
		}
		else if (kState.mouse.GetButtonState(MouseCode::Right) == On) {
			mouse_button = MouseOperation::RightHold;
		}
		else if (kState.mouse.GetButtonState(MouseCode::Left) == Down) {
			mouse_button = MouseOperation::LeftOn;
		}
		else {
			mouse_button = MouseOperation::Non;
		}
	}
	
	//入力方向を決める
	if (kState.keyboard.GetKeyState(KeyCode::W) == Down) {
		lastInput = Direction::Top;
	}
	if (kState.keyboard.GetKeyState(KeyCode::A) == Down) {
		lastInput = Direction::Left;
	}
	if (kState.keyboard.GetKeyState(KeyCode::S) == Down) {
		lastInput = Direction::Down;
	}
	if (kState.keyboard.GetKeyState(KeyCode::D) == Down) {
		lastInput = Direction::Right;
	}

	if (kState.keyboard.GetKeyState(KeyCode::P) == Down) {
		DeleteAllNotes();
	}

	if (kState.keyboard.GetKeyState(KeyCode::Enter) == On) {
		enter_on = true;
	}
	else {
		enter_on = false;
	}
}

void NotesManager::UpdateActor() {
	//if (!stopping) { return; } //止まっているとき以外は入力ができない
	int AmountofNotes = owner->GetWindowSize().h / NOTES_SIZE;
	float past = time * bpm / 60.0f;
	float firstNotesPos = -past * NOTES_SIZE - 270.f; //0秒におけるノーツラインの位置
	int pastNotesCnt = 0;
	while (firstNotesPos <= -NOTES_SIZE) {
		firstNotesPos += NOTES_SIZE;
		pastNotesCnt++;
	} 
	//firstNotesPosは、ここで実質的に画面内の一番下のノーツラインの位置になる

	//カーソルに合わせた当たり判定の移動
	selectingBox = Rect(0, int(firstNotesPos - NOTES_SIZE / 2 - 270), NOTES_SIZE * 4, NOTES_SIZE);
	selectingBox.x = mousePos.x >= 0.f ? 0 : -NOTES_SIZE * 4;
	int selectNotesNum = pastNotesCnt - 9;
	for (int i = 0; i <= AmountofNotes + 1; ++i) {
		if (selectingBox.Contain(mousePos)) { 
			selectNotesNum += i;
			break; 
		}
		selectingBox.y += NOTES_SIZE;
	}

	//ノーツを置く or 既にあるノーツを排除する
	if (mouse_button == MouseOperation::LeftOn) {
		//std::cout << "SelectNotesNum : " << selectNotesNum << "\n\n";
		int time = selectNotesNum;
		int dir = (int)lastInput;
		int lane = mousePos.x >= 0 ? 1 : 0;
		int type = 0;

		auto it = std::find_if(mNotes.begin(), mNotes.end(), [time, lane](Notes* n) {return n->time == time && n->lane == lane; });
		if (it != mNotes.end()) {
			delete* it;
		}
		else {
			CreateNotes(time, dir, type, lane);
		}
	}
	if (mouse_button == MouseOperation::RightOn) {
		int time = selectNotesNum;
		int lane = mousePos.x >= 0 ? 1 : 0;
		auto it = std::find_if(mNotes.begin(), mNotes.end(), [time, lane](Notes* n) {return n->time == time && n->lane == lane; });
		if (it != mNotes.end()) {
			notes_erase = true;
		}
		else {
			notes_erase = false;
			rush_mode = enter_on;
		}
	}
	if (mouse_button == MouseOperation::RightHold) {
		int time = selectNotesNum;
		int dir = (int)lastInput;
		int lane = mousePos.x >= 0 ? 1 : 0;
		int type = 1;

		auto it = std::find_if(mNotes.begin(), mNotes.end(), [time, lane](Notes* n) {return n->time == time && n->lane == lane; });
		if (it != mNotes.end()) {
			if (notes_erase) {
				delete* it;
			}
		}
		else {
			if (!notes_erase) {
				if (rush_mode) {
					CreateNotes(time, -1, 2, lane);
				}
				else {
					CreateNotes(time, dir, type, lane);
				}
			}
		}
	}
}
void NotesManager::Render2D() {
	Rect bd_(-480, -270, 960, 0);
	Rect bs_(0, 0, 64, 64);
	bd_.h = NOTES_SIZE;
	red_box->Draw(bd_, bs_, Color(1.f, 1.f, 1.f, .5f));

	green_box->Draw(selectingBox, bs_, Color(1.f, 1.f, 1.f, .5f));

	float nps = bpm / 60.0f; //1秒辺りのノーツ個数
	float past = time * nps; //過ぎたノーツの個数
	int AmountofNotes = owner->GetWindowSize().h / NOTES_SIZE;

	Rect draw(-NOTES_SIZE * 4, -272, NOTES_SIZE * 8, 4);
	Rect src(0, 0, 240, 4);
	draw.Offset(0.f, -NOTES_SIZE / 2 - past * NOTES_SIZE);

	std::vector<Notes*> currNotes;
	currNotes.clear();
	//auto it = currNotes.begin();

	for (int i = 0; i < (int)past + AmountofNotes + 1; ++i) {
		draw.Offset(0, NOTES_SIZE);
		if (draw.y < -275) { continue; }
		if (i % shosetsu_num == 0)
			bpm_line->Draw(draw, src);
		else 
		bpm_line->Draw(draw, src, little_clear_color);

		//back_inserterを使った処理に変更
//		it = std::copy_if(mNotes.begin(), mNotes.end(), it, [i](Notes* n) { return n->time == i; });
		std::copy_if(mNotes.begin(), mNotes.end(), std::back_inserter(currNotes), [i](Notes* n) { return n->time == i; });
		for (auto n : currNotes) {
			Rect nd_(-NOTES_SIZE * 2, -NOTES_SIZE / 2, NOTES_SIZE, NOTES_SIZE);
			Rect ns_(0, 0, 64, 64);
			nd_.y += draw.y;
			nd_.x += NOTES_SIZE * 4 * n->lane;
			if (n->type == NotesType::Rush) {
				ns_.Offset(0, 64 * 2);
			}
			else {
				int offsetX = (int)n->dir * 64;
				int offsetY = (int)n->type * 64;
				ns_.x += offsetX;
				ns_.y += offsetY;
			}
			notes_img->Draw(nd_, ns_);
		}
		currNotes.clear();
	}
	leftTime = time;
}

void NotesManager::DeleteAllNotes() {
	while (!mNotes.empty()) {
		delete mNotes.back();
	}
}