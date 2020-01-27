
// Opengl_Game.h : PROJECT_NAME アプリケーションのメイン ヘッダー ファイルです。
//

#pragma once

#ifndef __AFXWIN_H__
	#error "PCH に対してこのファイルをインクルードする前に 'stdafx.h' をインクルードしてください"
#endif

#include "resource.h"		// メイン シンボル


// COpengl_GameApp:
// このクラスの実装については、Opengl_Game.cpp を参照してください。
//

class COpengl_GameApp : public CWinApp
{
public:
	COpengl_GameApp();

// オーバーライド
public:
	virtual BOOL InitInstance();

// 実装

	DECLARE_MESSAGE_MAP()
};

extern COpengl_GameApp theApp;