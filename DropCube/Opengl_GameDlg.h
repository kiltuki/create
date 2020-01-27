
// Opengl_GameDlg.h : �w�b�_�[ �t�@�C��
//

#pragma once
#include "afxwin.h"

// COpengl_GameDlg �_�C�A���O
class COpengl_GameDlg : public CDialogEx
{
	// �R���X�g���N�V����
public:
	COpengl_GameDlg(CWnd* pParent = NULL);	// �W���R���X�g���N�^�[

	// �_�C�A���O �f�[�^
	enum { IDD = IDD_OPENGL_GAME_DIALOG };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV �T�|�[�g


	// ����
protected:
	HICON m_hIcon;

	// �������ꂽ�A���b�Z�[�W���蓖�Ċ֐�
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
