#pragma once
#include "SDL3/SDL.h"
#include "Math.h"


enum class KeyCode {
	A = SDL_SCANCODE_A,
	B = SDL_SCANCODE_B,
	C = SDL_SCANCODE_C,
	D = SDL_SCANCODE_D,
	E = SDL_SCANCODE_E,
	F = SDL_SCANCODE_F,
	G = SDL_SCANCODE_G,
	H = SDL_SCANCODE_H,
	I = SDL_SCANCODE_I,
	J = SDL_SCANCODE_J,
	K = SDL_SCANCODE_K,
	L = SDL_SCANCODE_L,
	M = SDL_SCANCODE_M,
	N = SDL_SCANCODE_N,
	O = SDL_SCANCODE_O,
	P = SDL_SCANCODE_P,
	Q = SDL_SCANCODE_Q,
	R = SDL_SCANCODE_R,
	S = SDL_SCANCODE_S,
	T = SDL_SCANCODE_T,
	U = SDL_SCANCODE_U,
	V = SDL_SCANCODE_V,
	W = SDL_SCANCODE_W,
	X = SDL_SCANCODE_X,
	Y = SDL_SCANCODE_Y,
	Z = SDL_SCANCODE_Z,

	Num0 = SDL_SCANCODE_0,
	Num1 = SDL_SCANCODE_1,
	Num2 = SDL_SCANCODE_2,
	Num3 = SDL_SCANCODE_3,
	Num4 = SDL_SCANCODE_4,
	Num5 = SDL_SCANCODE_5,
	Num6 = SDL_SCANCODE_6,
	Num7 = SDL_SCANCODE_7,
	Num8 = SDL_SCANCODE_8,
	Num9 = SDL_SCANCODE_9,

	Space = SDL_SCANCODE_SPACE,
	Enter = SDL_SCANCODE_RETURN,
	LShift = SDL_SCANCODE_LSHIFT,
	RShift = SDL_SCANCODE_RSHIFT,
	Comma = SDL_SCANCODE_COMMA,
	Period = SDL_SCANCODE_PERIOD,
	Slash = SDL_SCANCODE_SLASH,
	BackSlash = SDL_SCANCODE_BACKSLASH,
	Tab = SDL_SCANCODE_TAB,
	LCtrl = SDL_SCANCODE_LCTRL,
	RCtrl = SDL_SCANCODE_RCTRL,
	Alt = SDL_SCANCODE_ALTERASE,
	BackSpace = SDL_SCANCODE_BACKSPACE,
	Esc = SDL_SCANCODE_ESCAPE,
	LeftArrow = SDL_SCANCODE_LEFT,
	RightArrow = SDL_SCANCODE_RIGHT,
	UpArrow = SDL_SCANCODE_UP,
	DownArrow = SDL_SCANCODE_DOWN
};

enum class MouseCode {
	Left = SDL_BUTTON_LMASK,
	Wheel = SDL_BUTTON_MMASK,
	Right = SDL_BUTTON_RMASK,
	X1 = SDL_BUTTON_X1MASK,
	X2 = SDL_BUTTON_X2MASK
};

enum ButtonState {
	Off, Down, On, Up
};
class KeyboardState {
public:
	friend class InputSystem;
	KeyboardState() : mCurrState(nullptr), mPrevState{} {}

	//キーが押されているかだけを判別
	bool GetKeyValue(SDL_Scancode keyCode) const;

	//ひとつ前のフレームと現在のフレームから、キーの状態を判別
	ButtonState GetKeyState(SDL_Scancode keyCode) const;

	bool GetKeyValue(KeyCode code) const;

	ButtonState GetKeyState(KeyCode code) const;

private:
	int keyboardLength = SDL_SCANCODE_COUNT;
	//bool mCurrState[SDL_SCANCODE_COUNT];
	const bool* mCurrState;
	bool mPrevState[SDL_SCANCODE_COUNT];
};

class MouseState {
public:
	MouseState() : mMousePos(), mCurrButtons(0), mPrevButtons(0), isRelative(false), mScrollWheel() {}
	friend class InputSystem;

	const Vector2& GetPosition() const { return mMousePos; }
	bool GetButtonValue(int button) const;
	ButtonState GetButtonState(int button) const;
	bool GetButtonValue(MouseCode code) const;
	ButtonState GetButtonState(MouseCode code) const;
	Vector2 GetScrollWheel() const { return mScrollWheel; }

private:
	Vector2 mMousePos;
	Uint32 mCurrButtons;
	Uint32 mPrevButtons;
	bool isRelative;
	Vector2 mScrollWheel;
};

class InputState {
public:
	KeyboardState keyboard;
	MouseState mouse;
};

class InputSystem {
public:
	InputSystem(class Game* owner_) : owner(owner_) { Initialize(); }
	bool Initialize();
	void Shutdown();

	void PrepareForUpdate();
	void Update();

	const InputState& GetState() const { return state; }
	ButtonState GetKeyStates(SDL_Scancode keyCode) const { return state.keyboard.GetKeyState(keyCode); }
	void ProcessEvent(SDL_Event& event);
private:
	void SetRelativeMouseMode(SDL_Window* window, bool value);

	class Game* owner;
	InputState state;
};
