#include "Game.h"
#include "Actor.h"

Game::Game() :
	window(nullptr),
	windowSize(Rect(0, 0, 960, 540)),
	spriteShader(),
	isRunning(false),
	TicksCnt(0),
	updatingActors(false),
	spriteVerts(),
	context(),
	mInputSystem(),
	deltaTime(0)
{
}

GLuint Game::CompileShader(GLenum type, const char* src) {
	GLuint shader = glCreateShader(type);
	glShaderSource(shader, 1, &src, NULL);
	glCompileShader(shader);
	return shader;
}

bool Game::Init() { //初期化処理
	if (!SDL_Init(SDL_INIT_EVENTS)) {
		SDL_Log("Fatal Error! :SDL was NOT inited");
		return false;
	}
	window = SDL_CreateWindow("_Game", windowSize.w, windowSize.h, SDL_WINDOW_OPENGL);
	if (!window) {
		SDL_Quit();
		return false;
	}
	context = SDL_GL_CreateContext(window);

	SDL_GL_SetAttribute(SDL_GL_CONTEXT_PROFILE_MASK, SDL_GL_CONTEXT_PROFILE_CORE);
	SDL_GL_SetAttribute(SDL_GL_CONTEXT_MAJOR_VERSION, 3);
	SDL_GL_SetAttribute(SDL_GL_CONTEXT_MINOR_VERSION, 3);

	SDL_GL_SetAttribute(SDL_GL_RED_SIZE, 8);
	SDL_GL_SetAttribute(SDL_GL_GREEN_SIZE, 8);
	SDL_GL_SetAttribute(SDL_GL_BLUE_SIZE, 8);
	SDL_GL_SetAttribute(SDL_GL_ALPHA_SIZE, 8);

	SDL_GL_SetAttribute(SDL_GL_DOUBLEBUFFER, 1);
	SDL_GL_SetAttribute(SDL_GL_ACCELERATED_VISUAL, 1);

	TicksCnt = SDL_GetTicks();

	glewExperimental = GL_TRUE;
	if (glewInit() != GLEW_OK) {
		SDL_Log("Error :GLEW was not inited");
		return false;
	}
	glGetError(); //無害なエラーを取り除く
	if (!LoadShaders()) {
		SDL_Log("Error :shader has an error");
		return false;
	}
	isRunning = true;
	CreateVertexArray();
	mInputSystem = new InputSystem(this);
	Initialize();

	return true;
}

void Game::RunLoop() { //ゲーム全体のループ管理
	while (isRunning) {
		Input();
		Update();
		Render();
	}
}

void Game::Input() { //入力
	mInputSystem->PrepareForUpdate(); //最後のフレームの入力情報を保存

	//ポールイベント処理
	SDL_Event sEvent;
	while (SDL_PollEvent(&sEvent)) {
		switch (sEvent.type) {
		case SDL_EVENT_QUIT:
			isRunning = false;
			break;
		case SDL_EVENT_MOUSE_WHEEL:
			mInputSystem->ProcessEvent(sEvent);
			break;
		}
	}

	mInputSystem->Update(); //入力の更新

	const InputState& state = mInputSystem->GetState();
	if (state.keyboard.GetKeyState(SDL_SCANCODE_ESCAPE) == Up) {
		isRunning = false;
	}

	updatingActors = true;
	for (auto act : actors) {
		act->ProcessInput(state);
	}
	updatingActors = false;
}

void Game::Update() { //ゲーム本編の更新処理
	float dt = (SDL_GetTicks() - TicksCnt) / 1000.0f;
	TicksCnt = SDL_GetTicks();
	if (dt > 0.05f) {
		dt = 0.05f;
	}
	deltaTime = dt;
	UpdateActors();
	UpdateGame();
}

void Game::Render() { //描画全般処理
	glClearColor(0.2f, 0.2f, 0.2f, 1);
	glClear(GL_COLOR_BUFFER_BIT);
	// ToDo:シーン描画
	glEnable(GL_BLEND);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

	spriteShader->SetActive();
	for (auto sprite : sprites) {
		spriteVerts->SetActive();
		sprite->GetOwner()->RenderActor();
	}
	SDL_GL_SwapWindow(window);
}

void Game::Shutdown() { //片付け
	delete spriteShader, spriteVerts, context;
	while (!actors.empty()) {
		delete actors.back();
	}

	mInputSystem->Shutdown();
	SDL_GL_DestroyContext(context);
	SDL_DestroyWindow(window);
	SDL_Quit();
}

void Game::AddSprite(SpriteComponent* c) {
	auto it = sprites.begin();
	for (it = sprites.begin(); it != sprites.end(); ++it) {
		if (c->GetDrawOrder() < (*it)->GetDrawOrder()) {
			break;
		}
	}
	sprites.insert(it, c);
}
void Game::RemoveSprite(SpriteComponent* c) {
	auto it = std::find(sprites.begin(), sprites.end(), c);
	sprites.erase(it);
}
void Game::AddActor(Actor* a) {
	if (updatingActors) {
		pendingActors.emplace_back(a);
	}
	else {
		actors.emplace_back(a);
	}
}
void Game::RemoveActor(Actor* a) {
	std::vector<Actor*>::iterator it;
	if (updatingActors) {
		it = std::find(pendingActors.begin(), pendingActors.end(), a);
		pendingActors.erase(it);
	}
	else {
		it = std::find(actors.begin(), actors.end(), a);
		actors.erase(it);
	}
}
void Game::UpdateActors() {
	updatingActors = true;
	for (auto a : actors) {
		//a->ComputeWorldTransform();
		a->Update();
	}
	for (auto p : pendingActors) {
		//p->ComputeWorldTransform();
		actors.emplace_back(p);
	}
	updatingActors = false;
	pendingActors.clear();

	std::vector<Actor*> deadActors;
	for (auto a : actors) {
		if (a->GetState() == Actor::Dead) {
			deadActors.emplace_back(a);
		}
	}

	for (auto d : deadActors) {
		delete d;
	}
}

bool Game::LoadShaders() {
	spriteShader = new Shader();
	if (!spriteShader->MakeShader("./data/Sprite.vert", "./data/Sprite.frag")) {
		return false;
	}
	spriteShader->SetActive();
	mat4 viewProj = GetViewTransform();
	spriteShader->SetMatrixUniform("ViewProj", viewProj);
	return true;
}
mat4 Game::GetViewTransform() const {
	return mat4::CreateMatrixfor(2.0f / (float)windowSize.w, 2.0f / (float)windowSize.h);
}
void Game::CreateVertexArray() {
	float vertices[] = {
		-0.5f,  0.5f, 0.f, 0.f, 0.f, //左上
		 0.5f,  0.5f, 0.f, 1.f, 0.f, //右上
		 0.5f, -0.5f, 0.f, 1.f, 1.f, //右下
		-0.5f, -0.5f, 0.f, 0.f, 1.f  //左下
	};
	unsigned int indices[] = {
		0, 1, 2, 2, 3, 0
	};
	spriteVerts = new VertexArray(vertices, 4, indices, 6);
}