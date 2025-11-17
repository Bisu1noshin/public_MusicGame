#include "FManager.h"



FManager::FManager(Game* owner) 
	: Actor(owner), 
	sound(nullptr),
	system(nullptr),
	channel(nullptr),
	bpm(0),
	ss()
{
	FMOD_Init();
	mNotesManager = owner->GetActor<NotesManager>("NManager");

	//既存のデータの読み込み
	std::ifstream ifs = std::ifstream(filename);
	if (!ifs.is_open() || ifs.bad() || ifs.fail()) { 
		std::cout << "Fatal Eroor! : " << filename << " has an error" << std::endl;
		return; 
	}
	mNotesManager->ClearNotes();
	std::string str;

	//BPM入力
	std::getline(ifs, str); //一行目はスキップ
	std::getline(ifs, str);
	InputStringStream(str, bpm);

	//ノーツ入力
	while (std::getline(ifs, str)) {
		InputNotes(str);
	}
	ifs.close();
	time = 0;
	mNotesManager->SetBpm(bpm);
	mNotesManager->SetTime(time);
	unsigned int length;
	sound->getLength(&length, FMOD_TIMEUNIT_MS);
	mNotesManager->SetLength(length / 1000.f);
	std::cout << "BPM : " << bpm << std::endl;
	std::cout << "Length : " << length / 1000.f << std::endl;
}

FManager::~FManager() {
	
	std::ofstream ofs = std::ofstream(filename, std::ios::out);
	if (!ofs.is_open()) { return; }

	ofs << "bpm:\n" << bpm << "\n"; //BPMを記録
	ofs << mNotesManager->CreateNotesStr();
	ofs.close();

	sound->release();
	system->close();
	system->release();
}

void FManager::InputNotes(const std::string& string) {
	std::istringstream iss(string);
	std::stringstream ss;
	int splitCnt = 0;
	std::string subStr;
	int time = 0, dirN = 0, typeN = 0, lane = 0;
	while (std::getline(iss, subStr, ',')) {
		switch (splitCnt) {
		case 0:
			InputStringStream(subStr, time);
			break;
		case 1:
			InputStringStream(subStr, dirN);
			break;
		case 2:
			InputStringStream(subStr, typeN);
			break;
		case 3:
			InputStringStream(subStr, lane);
			break;
		}
		splitCnt++;
	}
	mNotesManager->CreateNotes(time, dirN, typeN, lane);
}

void FManager::FMOD_Init() {
	FMOD_RESULT re = FMOD::System_Create(&system);
	FMOD_Check(re);
	re = system->init(32, FMOD_INIT_NORMAL, nullptr);
	FMOD_Check(re);

	std::FILE* f = fopen(musicName.c_str(), "r");
	if (f == nullptr) { std::cout << "File isn't exist : " << musicName << std::endl; }

	re = system->createSound(musicName.c_str(), FMOD_DEFAULT, nullptr, &sound);
	FMOD_Check(re);
	if (re != FMOD_OK) { std::cout << "Failed to create stream\n"; }
	re = system->playSound(sound, nullptr, false, &channel);
	FMOD_Check(re);
	std::cout << "[←]5秒戻る　[→]5秒進む　[space]止める\n";
	std::cout << "[左クリック]フリックノーツ配置　[右クリックドラッグ]ホールドノーツ配置orノーツ消去\n";
	std::cout << "[右クリックドラッグ & Enterキー]ラッシュノーツ配置orノーツ消去\n";
}
void FManager::FMOD_Check(FMOD_RESULT result) {
	if (result != FMOD_OK) {
		std::cerr << "FMOD Error! : " << result << FMOD_ErrorString(result) << std::endl;
	}
}

void FManager::UpdateActor() {
	system->update();
	if (owner->GetKeyBoard()->GetKeyStates(SDL_SCANCODE_LEFT) == Down) { //5秒戻る
		BackTime(short_skip_time * 10);
	}
	Vector2 wheel = owner->GetKeyBoard()->GetState().mouse.GetScrollWheel();
	if (wheel.y < 0) {
		BackTime(short_skip_time);
	}
	if (owner->GetKeyBoard()->GetKeyStates(SDL_SCANCODE_RIGHT) == Down) { //5秒進む
		ForwardTime(short_skip_time * 10);
	}
	if (wheel.y > 0) {
		ForwardTime(short_skip_time);
	}
	if (owner->GetKeyBoard()->GetKeyStates(SDL_SCANCODE_SPACE) == Down) { //再生＆一時停止
		bool paused;
		channel->getPaused(&paused);
		channel->setPaused(!paused);
		stopping = !paused;
	}
	unsigned int pos; //進行時間を更新
	channel->getPosition(&pos, FMOD_TIMEUNIT_MS);
	time = (float)pos / 1000.f;

	mNotesManager->SetStopping(stopping);
	mNotesManager->SetTime(time); //NotesManagerを同期
}
void FManager::Render2D() {

}

void FManager::BackTime(unsigned int miliSec) {
	unsigned int pos;
	channel->getPosition(&pos, FMOD_TIMEUNIT_MS);
	pos = (pos > miliSec) ? pos - miliSec : 0;
	channel->setPosition(pos, FMOD_TIMEUNIT_MS);
}

void FManager::ForwardTime(unsigned int miliSec) {
	unsigned int pos, length;
	channel->getPosition(&pos, FMOD_TIMEUNIT_MS);
	sound->getLength(&length, FMOD_TIMEUNIT_MS);
	pos = (pos + miliSec < length) ? pos + miliSec : length;
	channel->setPosition(pos, FMOD_TIMEUNIT_MS);
}