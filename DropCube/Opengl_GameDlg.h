
// Opengl_GameDlg.h : ヘッダー ファイル
//

#pragma once
#include "afxwin.h"

// COpengl_GameDlg ダイアログ
class COpengl_GameDlg : public CDialogEx
{
	// コンストラクション
public:
	COpengl_GameDlg(CWnd* pParent = NULL);	// 標準コンストラクター

	// ダイアログ データ
	enum { IDD = IDD_OPENGL_GAME_DIALOG };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV サポート


	// 実装
protected:
	HICON m_hIcon;

	// 生成された、メッセージ割り当て関数
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
private:
	CStatic m_glView;
	CDC* m_pDC;
	HGLRC m_GLRC;
public:
	afx_msg void OnStnClickedGlview();
private:
	bool SetUpPixelFormat(HDC hdc);
public:
	void OnDestroy(void);
	afx_msg void OnBtnClickedRed();
	afx_msg void OnBtnClickedYellow();
	afx_msg void OnBtnClickedGreen();
	afx_msg void OnBtnClickedCyan();
	afx_msg void OnBtnClickedBlue();
	afx_msg void OnBtnStart();
	void SetUpdateRender(void);
	int getRandom(int min, int max);
	void drawScore();
	void drawHp();
	void gameOver();
	void reStart();
	void init();
	void SetRedRender(bool click);
	void SetYellowRender(bool click);
	void SetGreenRender(bool click);
	void SetCyanRender(bool click);
	void SetBlueRender(bool click);
	afx_msg LRESULT OnMessageRCV(WPARAM wParam, LPARAM lParam);
	afx_msg LRESULT OnMyTimer(WPARAM wParam,LPARAM lParam);
	afx_msg LRESULT OnSound(WPARAM wParam,LPARAM lParam);
	void CubeRender(int pos, int color);
	CStatic IDC_score;
};
