#include "InputSystem.h"

#ifndef SDL_ENABLE_OLD_NAMES
#define SDL_ENABLE_OLD_NAMES
#include "SDL3/SDL_oldnames.h"
#endif

bool KeyboardState::GetKeyValue(SDL_Scancode keyCode) const {
	return mCurrState[keyCode];
}
bool KeyboardState::GetKeyValue(KeyCode code) const {
	return mCurrState[(int)code];
}
ButtonState KeyboardState::GetKeyState(SDL_Scancode keyCode) const {
	if (!mPrevState[keyCode]) {
		if (!mCurrState[keyCode]) {
			return Off;
		}
		else {
			return Down;
		}
	}
	else {
		if (!mCurrState[keyCode]) {
			return Up;
		}
		else {
			return On;
		}
	}
}
ButtonState KeyboardState::GetKeyState(KeyCode code) const {
	int kc = (int)code;
	if (!mPrevState[kc]) {
		if (!mCurrState[kc]) {
			return Off;
		}
		else {
			return Down;
		}
	}
	else {
		if (!mCurrState[kc]) {
			return Up;
		}
		else {
			return On;
		}
	}
}


bool MouseState::GetButtonValue(int button) const {
	return SDL_BUTTON_MASK(button) != 0;
}
bool MouseState::GetButtonValue(MouseCode code) const {
	return SDL_BUTTON_MASK((int)code) != 0;
}

ButtonState MouseState::GetButtonState(int button) const {
	int mCurrOn = mCurrButtons & button;
	int mPrevOn = mPrevButtons & button;

	if (mCurrOn != 0) {
		if (mPrevOn != 0) {
			return On;
		}
		else {
			return Down;
		}
	}
	if (mPrevOn != 0) {
		return Up;
	}
	else {
		return Off;
	}
}
ButtonState MouseState::GetButtonState(MouseCode code) const {
	int mCurrOn = mCurrButtons & (int)code;
	int mPrevOn = mPrevButtons & (int)code;

	if (mCurrOn != 0) {
		if (mPrevOn != 0) {
			return On;
		}
		else {
			return Down;
		}
	}
	if (mPrevOn != 0) {
		return Up;
	}
	else {
		return Off;
	}
}


bool InputSystem::Initialize() {
	state = InputState();
	state.keyboard.mCurrState = SDL_GetKeyboardState(&state.keyboard.keyboardLength);
	memset(state.keyboard.mPrevState, 0, SDL_SCANCODE_COUNT);
	return true;
}
void InputSystem::Shutdown() {
}

void InputSystem::PrepareForUpdate() {
	memcpy(state.keyboard.mPrevState, state.keyboard.mCurrState, SDL_SCANCODE_COUNT);
	state.mouse.mPrevButtons = state.mouse.mCurrButtons;
	state.mouse.mScrollWheel = Vector2();
}

void InputSystem::Update() {
	state.keyboard.mCurrState = SDL_GetKeyboardState(NULL);
	
	if (state.mouse.isRelative) {
		state.mouse.mCurrButtons = SDL_GetRelativeMouseState(&state.mouse.mMousePos.x, &state.mouse.mMousePos.y);
	}
	else {
		state.mouse.mCurrButtons = SDL_GetMouseState(&state.mouse.mMousePos.x, &state.mouse.mMousePos.y);
	}

	//マウス座標をスクリーン座標に修正
	state.mouse.mMousePos.x -= 480.f;
	state.mouse.mMousePos.y = state.mouse.mMousePos.y * -1.f + 270.f;
}

void InputSystem::SetRelativeMouseMode(SDL_Window* window, bool value) {
	SDL_SetWindowRelativeMouseMode(window, value);
	state.mouse.isRelative = value;
}

void InputSystem::ProcessEvent(SDL_Event& event) {
	switch (event.type) {
	case SDL_EVENT_MOUSE_WHEEL:
		state.mouse.mScrollWheel = Vector2(event.wheel.x, event.wheel.y);
		break;
	default:
		break;
	}
}