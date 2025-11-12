#version 330

//
//shader.fragment
//
in vec2 fragTexCoord;
uniform sampler2D uTex;
uniform vec4 tintColor;

out vec4 outColor;

void main(){
	vec4 tColor = texture(uTex, fragTexCoord);
	outColor = tColor * tintColor;
}