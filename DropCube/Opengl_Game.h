
// Opengl_Game.h : PROJECT_NAME �A�v���P�[�V�����̃��C�� �w�b�_�[ �t�@�C���ł��B
//

#pragma once

#ifndef __AFXWIN_H__
	#error "PCH �ɑ΂��Ă��̃t�@�C�����C���N���[�h����O�� 'stdafx.h' ���C���N���[�h���Ă�������"
#endif

#include "resource.h"		// ���C�� �V���{��


// COpengl_GameApp:
// ���̃N���X�̎����ɂ��ẮAOpengl_Game.cpp ���Q�Ƃ��Ă��������B
//

class COpengl_GameApp : public CWinApp
{
public:
	COpengl_GameApp();

// �I�[�o�[���C�h
public:
	virtual BOOL InitInstance();

// ����

	DECLARE_MESSAGE_MAP()
};

extern COpengl_GameApp theApp;