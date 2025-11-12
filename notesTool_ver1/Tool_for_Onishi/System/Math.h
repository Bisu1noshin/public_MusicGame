#pragma once
#include "Coordinate.h"
/// <summary>
/// 数学的な関数をカバーするヘッダ
/// Coordinate.hよりこっちをインクルードしてほしい
/// </summary>

namespace Math {

	inline static float Clamp(float value, float max, float min) { //値をmin-max間に制限する
		return value > max ? max : value < min ? min : value;
	}
	inline static int Clamp(int value, int max, int min) {
		return value > max ? max : value < min ? min : value;
	}

	inline static float Clamp01(float value) { //値を0-1間に制限する
		return value > 1.0f ? 1.0f : value < 0.0f ? 0.0f : value;
	}

	inline static bool NearZero(float f, float epsilon = 1e-6f) { // 1/100,000以下を0とみなす　閾値は変更可
		return fabsf(f) <= epsilon;
	}

	inline static float Lerp(float a, float b, float t) { //線形補完
		return a + (b - a) * Clamp01(t);
	}

	inline static Vector2 Lerp(Vector2 a, Vector2 b, float t) {
		return a + (b - a) * Clamp01(t);
	}

	inline static Vector3 Lerp(Vector3 a, Vector3 b, float t) {
		return a + (b - a) * Clamp01(t);
	}

	inline static float Lerp(float a, float b, float t, float epsilon) { //線形補完後に値をクランプできる
		float x = Lerp(a, b, t);
		if (NearZero(1.0f - t, epsilon)) {
			x = b;
		}
		return x;
	}

	inline static Vector2 Lerp(Vector2 a, Vector2 b, float t, float epsilon) {
		Vector2 x = Lerp(a, b, t);
		if (NearZero(1.0f - t, epsilon)) {
			x = b;
		}
		return x;
	}

	inline static Vector3 Lerp(Vector3 a, Vector3 b, float t, float epsilon) {
		Vector3 x = Lerp(a, b, t);
		if (NearZero(1.0f - t, epsilon)) {
			x = b;
		}
		return x;
	}

	inline static float Min(float a, float b) { //小さい方の引数を返す
		return a > b ? b : a;
	}

	inline static float Max(float a, float b) { //大きい方の引数を返す
		return a > b ? a : b;
	}
}