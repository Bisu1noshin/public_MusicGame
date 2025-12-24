#include "System/Game.h"
#include "NotesManager.h"
#include "FManager.h"

//Šeístatic constƒƒ“ƒo‚Ì‰Šú‰»
#ifdef MY_MATH
#ifndef MY_MATH_INITED
#define MY_MATH_INITED
const Vector2 Vector2::UnitX(1, 0), Vector2::UnitY(0, 1);
const Vector3 Vector3::UnitX(1, 0, 0), Vector3::UnitY(0, 1, 0), Vector3::UnitZ(0, 0, 1);
const Quaternion Quaternion::identity(Vector3(), 1);
#endif
#endif

NotesManager* mNotesManager;
FManager* mFManager;

int main() {
	Game ge;
	if (ge.Init()) {
		ge.RunLoop();
	}
	ge.Shutdown();
	return 0;
}

void Game::Initialize() {
	mNotesManager = CreateActor<NotesManager>();
	mNotesManager->SetName("NManager");
	mFManager = CreateActor<FManager>();
	mFManager->SetName("FManager");
}
void Game::Finalize() {

}
void Game::UpdateGame() {

}