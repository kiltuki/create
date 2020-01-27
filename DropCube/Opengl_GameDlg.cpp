
// Opengl_GameDlg.cpp : �����t�@�C��
//

#include "stdafx.h"
#include "Opengl_Game.h"
#include "Opengl_GameDlg.h"

#include <Windows.h>
#include <process.h>
#include <time.h>
#include <stdio.h>
#include <stdlib.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

#define WM_RCV (WM_USER+1)
#define WM_RT (WM_USER+2)
#define WM_RCD (WM_)
#define TIMER_WAIT 33;//�^�C�}�[�̑҂�����
#define FIRST_STAGECLASS Stage1()//�ŏ��ɌĂ΂��X�e�[�W�N���X
#define CUBE_MAX 1


//�֐��̃v���g�^�C�v�錾

//�Q�[���̃��C���X���b�h�p�錾
HANDLE h_opengl = NULL;
HWND hDlg;
unsigned int tid;

//�^�C�}�[�X���b�h�p�錾
HANDLE h_timer = NULL;
int timercnt = 0;
bool timerFlg = false;

bool redClickFlg = false;
bool yellowClickFlg = false;
bool greenClickFlg = false;
bool cyanClickFlg = false;
bool blueClickFlg = false;
bool startFlg = false;
bool gameOverFlg = true;
int rpos = 0;
int	rcolor = 0;
double downspeed = 0.5;
double i = 0;
int j = 0;
int a = 0;
int score = 0;
int hp = 50;


//�����̏o���ꏊ
GLfloat cubePos[][3]={{-4.9,4.5,1.35},{-4.9,2.0,-0.3},  //�ԁ@�O�A���
{-2.5,2.0,-1.3},{-2.5,4.5,1.35},//���@���A�O
{0.0,4.5,1.35},{0.0,2.0,-1.3},    //�΁@�O�A���
{2.5,2.0,-1.3},{2.5,4.5,1.35},  //���@���A�O
{4.9,4.5,1.35},{4.9,2.0,-1.3}};   //�@�O�A���

//�����̂̐F
GLfloat colors[][3]={{1.0,0.0,0.0}, //��
{1.0,1.0,0.0}, //��
{0.0,1.0,0.0}, //��
{0.0,1.0,1.0}, //��
{0.0,0.0,1.0}, //��
{1.0,0.5,0.5}, //�s���N
{1.0,1.0,1.0}};	 //��

GLdouble floorPos[][3]={{-3.5,0.0,3.35},{-3.5,0.0,1.3},  //�ԁ@�O�A���
{-1.75,0.0,1.3},{-1.75,0.0,3.35},  //���@���A�O
{0.0,0.0,3.35},{0.0,0.0,1.3},    //�΁@�O�A���
{1.75,0.0,1.3},{1.75,0.0,3.35},    //���@���A�O
{3.5,0.0,3.35},{3.5,0.0,1.3}};   //�@�O�A���



int rPos[CUBE_MAX];
int rColor[CUBE_MAX];

int hScore_0 = 0;
int hScore_1 = 0;



// �A�v���P�[�V�����̃o�[�W�������Ɏg���� CAboutDlg �_�C�A���O




//-------------------------------------------------------- 
// �Q�[���p�}���`�X���b�h�𓮂������߂̃f�[�^�𑗂�
//-------------------------------------------------------- 


unsigned __stdcall dropCube(VOID* dummy){
	while(startFlg){
		SleepEx(300,TRUE);

		i+=downspeed;
		PostMessage(hDlg, WM_RCV, (WPARAM)i, NULL);

	}
	_endthreadex(0);
	return 0;
}


//--------------------------------------------------------
// �^�C�}�[�p�}���`�X���b�h�𓮂������߂̃f�[�^�𑗂�
//-------------------------------------------------------- 

unsigned __stdcall timer(VOID* dummy){
	while(timerFlg){
		Sleep(1000);
		timercnt++;
		PostMessage(hDlg,WM_RT,(WPARAM)timercnt,NULL);
	}
	_endthreadex(0);
	return 0;
}

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

	// �_�C�A���O �f�[�^
	enum { IDD = IDD_ABOUTBOX };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV �T�|�[�g

	// ����
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialogEx(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
END_MESSAGE_MAP()


// COpengl_GameDlg �_�C�A���O



COpengl_GameDlg::COpengl_GameDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(COpengl_GameDlg::IDD, pParent)
	, m_pDC(NULL)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void COpengl_GameDlg::DoDataExchange(CDataExchange* pDX)//dialog�ɒǉ�������̕ϐ���`
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_GLVIEW, m_glView);
	DDX_Control(pDX, IDC_SCORE, IDC_score);
}

BEGIN_MESSAGE_MAP(COpengl_GameDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()

	//AFX_MSG_MAP
	ON_WM_DESTROY()
	ON_BN_CLICKED(IDC_BUTTON5, &COpengl_GameDlg::OnBtnClickedRed)
	ON_BN_CLICKED(IDC_BUTTON8, &COpengl_GameDlg::OnBtnClickedYellow)
	ON_BN_CLICKED(IDC_BUTTON7, &COpengl_GameDlg::OnBtnClickedGreen)
	ON_BN_CLICKED(IDC_BUTTON9, &COpengl_GameDlg::OnBtnClickedCyan)
	ON_BN_CLICKED(IDC_BUTTON10, &COpengl_GameDlg::OnBtnClickedBlue)
	ON_BN_CLICKED(IDC_BUTTON4, &COpengl_GameDlg::OnBtnStart)
	ON_MESSAGE(WM_RCV,&COpengl_GameDlg::OnMessageRCV)
	ON_MESSAGE(WM_RT,&COpengl_GameDlg::OnMyTimer)
END_MESSAGE_MAP()


// COpengl_GameDlg ���b�Z�[�W �n���h���[

BOOL COpengl_GameDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	hDlg = this->m_hWnd;	

	// "�o�[�W�������..." ���j���[���V�X�e�� ���j���[�ɒǉ����܂��B

	// IDM_ABOUTBOX �́A�V�X�e�� �R�}���h�͈͓̔��ɂȂ���΂Ȃ�܂���B
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// ���̃_�C�A���O�̃A�C�R����ݒ肵�܂��B�A�v���P�[�V�����̃��C�� �E�B���h�E���_�C�A���O�łȂ��ꍇ�A
	//  Framework �́A���̐ݒ�������I�ɍs���܂��B
	SetIcon(m_hIcon, TRUE);			// �傫���A�C�R���̐ݒ�
	SetIcon(m_hIcon, FALSE);		// �������A�C�R���̐ݒ�

	// OpenGL�������������ɒǉ����܂��B
	m_pDC = new CClientDC(&m_glView);//������OpenGL��`�悵�܂���

	if(SetUpPixelFormat(m_pDC->m_hDC) != false){
		m_GLRC = wglCreateContext(m_pDC->m_hDC);
		wglMakeCurrent(m_pDC->m_hDC,m_GLRC);

		CRect rc;
		m_glView.GetClientRect(&rc);
		GLint width = rc.Width();
		GLint height = rc.Height();
		GLdouble aspect = (GLdouble)width/(GLdouble)height;

		glClearColor(0.0f,0.0f,0.0f,1.0f);
		glViewport(0,0,width,height);
		glMatrixMode(GL_PROJECTION);
		glLoadIdentity();

		gluPerspective( 30.0, aspect, 0.1, 100.0);
		gluLookAt( 0.0, 5.0, 10.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0); 

		glMatrixMode( GL_MODELVIEW);
		glLoadIdentity();
		init();



	}
	//�������͂����܂�

	return TRUE;  // �t�H�[�J�X���R���g���[���ɐݒ肵���ꍇ�������ATRUE ��Ԃ��܂��B
}

void COpengl_GameDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialogEx::OnSysCommand(nID, lParam);
	}
}

// �_�C�A���O�ɍŏ����{�^����ǉ�����ꍇ�A�A�C�R����`�悷�邽�߂�
//  ���̃R�[�h���K�v�ł��B�h�L�������g/�r���[ ���f�����g�� MFC �A�v���P�[�V�����̏ꍇ�A
//  ����́AFramework �ɂ���Ď����I�ɐݒ肳��܂��B

void COpengl_GameDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // �`��̃f�o�C�X �R���e�L�X�g

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// �N���C�A���g�̎l�p�`�̈���̒���
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// �A�C�R���̕`��
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
		//OpenGL

		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
		SwapBuffers(m_pDC->m_hDC);
	}
}

// ���[�U�[���ŏ��������E�B���h�E���h���b�O���Ă���Ƃ��ɕ\������J�[�\�����擾���邽�߂ɁA
//  �V�X�e�������̊֐����Ăяo���܂��B
HCURSOR COpengl_GameDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

/* OpenGL�ɂ������ʃ��[�h�̐ݒ���s���Ă���
��{�I�ɂ͂��̊֐��̒��g��ύX���邱�Ƃ͂Ȃ�*/
bool COpengl_GameDlg::SetUpPixelFormat(HDC hdc){
	PIXELFORMATDESCRIPTOR pfd = {
		sizeof(PIXELFORMATDESCRIPTOR),
		1,
		PFD_DRAW_TO_WINDOW | PFD_SUPPORT_OPENGL	|
		PFD_DOUBLEBUFFER | PFD_TYPE_RGBA,
		24,
		0,0,0,0,0,0,
		0,0,
		0,0,0,0,0,
		32,
		0,
		0,
		PFD_MAIN_PLANE,
		0,
		0,0,0
	};

	int pf = ChoosePixelFormat(hdc,&pfd);
	if(pf != 0) return SetPixelFormat(hdc,pf,&pfd);
	return false;

}

void COpengl_GameDlg::OnDestroy(){
	CDialog::OnDestroy();

	wglMakeCurrent(NULL,NULL);
	wglDeleteContext(m_GLRC);
	delete m_pDC;
}

//�����̂̏o���ʒu����������
void COpengl_GameDlg::init(){

	glEnable(GL_DEPTH_TEST);
	for(int a = 0; a<CUBE_MAX; a++){
		rcolor = getRandom(0,6);
		rpos = getRandom(0,9);

		rPos[a] =rpos;
		rColor[a] = rcolor;
	}
}

//�ԃ{�^���̏���
void COpengl_GameDlg::OnBtnClickedRed()
{
	if(!redClickFlg){
		redClickFlg = true;
		SetUpdateRender();

	}else{
		redClickFlg = false;
		SetUpdateRender();

	}
}

//���F�{�^���̏���
void COpengl_GameDlg::OnBtnClickedYellow()
{
	if(!yellowClickFlg){
		yellowClickFlg = true;
		SetUpdateRender();
	}else{
		yellowClickFlg = false;
		SetUpdateRender();

	}
}

//�΃{�^���̏���
void COpengl_GameDlg::OnBtnClickedGreen()
{
	if(!greenClickFlg){
		greenClickFlg = true;
		SetUpdateRender();

	}else{
		greenClickFlg = false;
		SetUpdateRender();

	}
}

//���F�{�^���̏���
void COpengl_GameDlg::OnBtnClickedCyan()
{
	if(!cyanClickFlg){
		cyanClickFlg = true;
		SetUpdateRender();

	}else{
		cyanClickFlg = false;
		SetUpdateRender();

	}
}

//�{�^���̏���
void COpengl_GameDlg::OnBtnClickedBlue()
{
	if(!blueClickFlg){
		blueClickFlg = true;
		SetUpdateRender();

	}else{
		blueClickFlg = false;
		SetUpdateRender();

	}
}

//�X�^�[�g�{�^���i�}���`�X���b�h�J�n�j
void COpengl_GameDlg::OnBtnStart()
{
	if(gameOverFlg == true){
		timercnt = 0;
		timerFlg = true;
		downspeed = 0.5;
		score = 0;
		hp = 50;


		glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT);
		redClickFlg = false;
		yellowClickFlg = false;
		greenClickFlg = false;
		cyanClickFlg = false;
		blueClickFlg = false;

		SetUpdateRender();
		startFlg = true;
		gameOverFlg = false;

		//�Q�[�����C���̃X���b�h���Ă�
		h_opengl = (HANDLE)_beginthreadex(NULL,0,dropCube,NULL,0,&tid);

		//�^�C�}�[�̃X���b�h���Ă�
		h_timer = (HANDLE)_beginthreadex(NULL,0,timer,NULL,0,&tid);
	}

}

//���̕`��X�V
void COpengl_GameDlg::SetUpdateRender()
{
	SetRedRender(redClickFlg);
	SetYellowRender(yellowClickFlg);
	SetGreenRender(greenClickFlg);
	SetCyanRender(cyanClickFlg);
	SetBlueRender(blueClickFlg);
	glFinish();
}

//�^�C�}�[�̏���
LRESULT COpengl_GameDlg::OnMyTimer(WPARAM wParam,LPARAM lParam){
	if(gameOverFlg == true){
		return 0;
	}
	if(timercnt % 10 ==0){
		downspeed += 0.1;
	}
	CString t_time;
	CWnd* timeDISP = GetDlgItem(IDC_TIMER);
	CClientDC timeDC(timeDISP);
	CRect timeRECT;

	timeDISP->GetClientRect(timeRECT);
	timeDC.FillSolidRect(timeRECT,RGB(255,255,255));

	CFont tFont, *oldFont;

	tFont.CreateFont(45,25,0,0,FW_NORMAL,FALSE,FALSE,FALSE,SHIFTJIS_CHARSET,
		OUT_DEFAULT_PRECIS,CLIP_DEFAULT_PRECIS,DEFAULT_QUALITY,DEFAULT_PITCH|FF_DONTCARE,_T("Arial"));

	t_time.Format(_T("%5d"), timercnt);
	oldFont = timeDC.SelectObject(&tFont);
	timeDC.SetTextColor(RGB(0,0,0));
	timeDC.TextOutW(0,0,t_time);
	timeDC.SelectObject(oldFont);
	return TRUE;
}

//�Q�[���̃��C���̏���
LRESULT COpengl_GameDlg::OnMessageRCV(WPARAM wParam, LPARAM lParam)
{
	if(gameOverFlg == true){
		return 0;
	}
	srand((unsigned int)time(NULL));

	glClear( GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glEnable(GL_DEPTH_TEST);
	rcolor = getRandom(0,6);
	rpos = getRandom(0,9);

	for(int a = 0; a<CUBE_MAX; a++){

		glPushMatrix();			
		if(i<=6){
			glTranslated(0.0,cubePos[rPos[a]][1]-i, 0.0); //���s�ړ��l�̐ݒ� 
			//			Beep(659, 110);//�~


		}else if(i>6){
			rPos[a] = getRandom(0,9);
			rColor[a] = getRandom(0,6);
			glTranslated(0.0, cubePos[rPos[a]][1]-i, 0.0); //���s�ړ��l�̐ݒ�

			i = 0;

			i+=downspeed;
		}  
		glRotatef(90,1.0,0.0,0.0);
		glScalef(0.7,0.7,0.7);

		CubeRender(rPos[a],rColor[a]);
		//			CubeRender(rpos,rcolor);
		glPopMatrix();

		//�����蔻��
		//���ƑΉ�����F�̗����̂�����������
		if(i>=3.5 && 4.5>= i){
			if(rPos[a]==0 && redClickFlg == false && rColor[a]==0 || rPos[a]==1 && redClickFlg == true && rColor[a]==0 ||
				rPos[a]==2 && yellowClickFlg == false && rColor[a]==1 || rPos[a]==3 && yellowClickFlg == true && rColor[a]==1 ||
				rPos[a]==4 && greenClickFlg == false && rColor[a]==2 || rPos[a]==5 && greenClickFlg == true && rColor[a]==2 ||
				rPos[a]==6 && cyanClickFlg == false && rColor[a]==3 || rPos[a]==7 && cyanClickFlg == true && rColor[a]==3 ||
				rPos[a]==8 && blueClickFlg == false && rColor[a]==4 || rPos[a]==9 && blueClickFlg == true && rColor[a]==4){

					Beep(900, 200);//�V

					rPos[a] = getRandom(0,9);	
					rColor[a] = getRandom(0,6);	  
					score += 10;			
					i=0;

					//���ƑΉ����Ȃ��F�̗����̂�����������
			}else if(rPos[a]==0 && redClickFlg == false && (rColor[a]==1 || rColor[a]==2 || rColor[a]==3 || rColor[a]==4)||
				rPos[a]==1 && redClickFlg == true && (rColor[a]==1 || rColor[a]==2 || rColor[a]==3 || rColor[a]==4) ||
				rPos[a]==2 && yellowClickFlg == false && (rColor[a]==0 || rColor[a]==2 || rColor[a]==3 || rColor[a]==4) ||
				rPos[a]==3 && yellowClickFlg == true && (rColor[a]==0 || rColor[a]==2 || rColor[a]==3 || rColor[a]==4) ||
				rPos[a]==4 && greenClickFlg == false && (rColor[a]==0 || rColor[a]==1 || rColor[a]==3 || rColor[a]==4) ||
				rPos[a]==5 && greenClickFlg == true && (rColor[a]==0 || rColor[a]==1 || rColor[a]==3 || rColor[a]==4) ||
				rPos[a]==6 && cyanClickFlg == false && (rColor[a]==0 || rColor[a]==1 || rColor[a]==2 || rColor[a]==4)||
				rPos[a]==7 && cyanClickFlg == true && (rColor[a]==0 || rColor[a]==1 || rColor[a]==2 || rColor[a]==4)||
				rPos[a]==8 && blueClickFlg == false && (rColor[a]==0 || rColor[a]==1 || rColor[a]==2 || rColor[a]==3) ||
				rPos[a]==9 && blueClickFlg == true && (rColor[a]==0 || rColor[a]==1 || rColor[a]==2 || rColor[a]==3)){
					Beep(415, 100);//�V
					Beep(440, 200);//�V

					rPos[a] = getRandom(0,9);	
					rColor[a] = getRandom(0,6);	
					hp -= 10;
					if(hp == 0){
						gameOver();
					}
					i=0;
					//�s���N�F�̗�����(�񕜃A�C�e��)������������
			}else if(rPos[a]==0 && redClickFlg == false && rColor[a]==5|| rPos[a]==1 && redClickFlg == true && rColor[a]==5 ||
				rPos[a]==2 && yellowClickFlg == false && rColor[a]==5 || rPos[a]==3 && yellowClickFlg == true && rColor[a]==5 ||
				rPos[a]==4 && greenClickFlg == false && rColor[a]==5 ||	rPos[a]==5 && greenClickFlg == true && rColor[a]==5 ||
				rPos[a]==6 && cyanClickFlg == false && rColor[a]==5 || rPos[a]==7 && cyanClickFlg == true && rColor[a]==5 ||
				rPos[a]==8 && blueClickFlg == false && rColor[a]==5 || rPos[a]==9 && blueClickFlg == true && rColor[a]==5){
					Beep(900, 200);//�V
					rPos[a] = getRandom(0,9);	
					rColor[a] = getRandom(0,6);	
					i=0;
					//�ߏ�񕜖h��
					if(hp<50){
						hp+=10;
					}
					//���F�̗����́i�����A�C�e���j������������
			}else if(rPos[a]==0 && redClickFlg == false && rColor[a]==6|| rPos[a]==1 && redClickFlg == true && rColor[a]==6 ||
				rPos[a]==2 && yellowClickFlg == false && rColor[a]==6 || rPos[a]==3 && yellowClickFlg == true && rColor[a]==6 ||
				rPos[a]==4 && greenClickFlg == false && rColor[a]==6 ||	rPos[a]==5 && greenClickFlg == true && rColor[a]==6 ||
				rPos[a]==6 && cyanClickFlg == false && rColor[a]==6 || rPos[a]==7 && cyanClickFlg == true && rColor[a]==6 ||
				rPos[a]==8 && blueClickFlg == false && rColor[a]==6 || rPos[a]==9 && blueClickFlg == true && rColor[a]==6){
					Beep(900, 200);//�V
					rPos[a] = getRandom(0,9);	
					rColor[a] = getRandom(0,6);	
					i=0;
					//�ߏ茸���h��
					if(downspeed>0.5){
						downspeed-=0.1;
					}
			}
		}
	}
	SetUpdateRender();

	drawScore();
	drawHp();
	glDisable(GL_DEPTH_TEST);
	SwapBuffers(m_pDC->m_hDC);	  

	return TRUE;

}

//�����̂�`�悷��
//�F�Əꏊ�͈����Ŏw��\

void COpengl_GameDlg::CubeRender(int pos, int color)
{

	glBegin(GL_QUAD_STRIP);
	glColor3f(colors[color][0],colors[color][1],colors[color][2]);
	glVertex3f(-0.5+cubePos[pos][0], 0.5+cubePos[pos][1],-0.5+cubePos[pos][2]);//3
	glVertex3f(-0.5+cubePos[pos][0], 0.5+cubePos[pos][1], 0.5+cubePos[pos][2]);//2
	glVertex3f( 0.5+cubePos[pos][0], 0.5+cubePos[pos][1],-0.5+cubePos[pos][2]);//4
	glVertex3f( 0.5+cubePos[pos][0], 0.5+cubePos[pos][1], 0.5+cubePos[pos][2]);//1	
	glVertex3f( 0.5+cubePos[pos][0],-0.5+cubePos[pos][1],-0.5+cubePos[pos][2]);//8
	glVertex3f( 0.5+cubePos[pos][0],-0.5+cubePos[pos][1], 0.5+cubePos[pos][2]);//5
	glVertex3f(-0.5+cubePos[pos][0],-0.5+cubePos[pos][1],-0.5+cubePos[pos][2]);//7
	glVertex3f(-0.5+cubePos[pos][0],-0.5+cubePos[pos][1], 0.5+cubePos[pos][2]);//6
	glVertex3f(-0.5+cubePos[pos][0], 0.5+cubePos[pos][1],-0.5+cubePos[pos][2]);//3
	glVertex3f(-0.5+cubePos[pos][0], 0.5+cubePos[pos][1], 0.5+cubePos[pos][2]);//2
	glVertex3f(-0.5+cubePos[pos][0], 0.5+cubePos[pos][1], 0.5+cubePos[pos][2]);//2
	glVertex3f(-0.5+cubePos[pos][0],-0.5+cubePos[pos][1], 0.5+cubePos[pos][2]);//6
	glVertex3f( 0.5+cubePos[pos][0], 0.5+cubePos[pos][1], 0.5+cubePos[pos][2]);//1	
	glVertex3f( 0.5+cubePos[pos][0],-0.5+cubePos[pos][1], 0.5+cubePos[pos][2]);//5
	glVertex3f(-0.5+cubePos[pos][0], 0.5+cubePos[pos][1],-0.5+cubePos[pos][2]);//3
	glVertex3f(-0.5+cubePos[pos][0],-0.5+cubePos[pos][1],-0.5+cubePos[pos][2]);//7
	glVertex3f( 0.5+cubePos[pos][0], 0.5+cubePos[pos][1],-0.5+cubePos[pos][2]);//4
	glVertex3f( 0.5+cubePos[pos][0],-0.5+cubePos[pos][1],-0.5+cubePos[pos][2]);//8		
	glEnd();

	glPopMatrix();
	glFinish();
}

//���������p

int COpengl_GameDlg::getRandom(int min, int max){
	return min + (int)(rand()*(max-min+1)/(1.0+RAND_MAX));
}

//�X�R�A����уn�C�X�R�A�̊Ǘ�������

void COpengl_GameDlg::drawScore(){
	CString t_score, t_hscore;
	CWnd* scoreDISP = GetDlgItem(IDC_SCORE);
	CWnd* hscoreDISP = GetDlgItem(IDC_HSCORE);
	CClientDC sDC(scoreDISP);
	CClientDC hsDC(hscoreDISP);
	CRect sRECT,hsRECT;
	CFont tFont, *oldFont,*oldFont_h;
	tFont.CreateFont(45,25,0,0,FW_NORMAL,FALSE,FALSE,FALSE,SHIFTJIS_CHARSET,
		OUT_DEFAULT_PRECIS,CLIP_DEFAULT_PRECIS,DEFAULT_QUALITY,DEFAULT_PITCH|FF_DONTCARE,_T("Arial"));

	//�X�R�A�`��
	scoreDISP->GetClientRect(sRECT);
	sDC.FillSolidRect(sRECT,RGB(255,255,255));
	t_score.Format(_T("%5d"), score);
	oldFont = sDC.SelectObject(&tFont);
	sDC.SetTextColor(RGB(0,0,0));
	sDC.TextOutW(0,0,t_score);
	sDC.SelectObject(oldFont);

	//�n�C�X�R�A�`��
	hscoreDISP->GetClientRect(hsRECT);
	hsDC.FillSolidRect(hsRECT,RGB(255,255,255));	
	t_hscore.Format(_T("%5d"),hScore_1);		
	oldFont_h = hsDC.SelectObject(&tFont);			
	hsDC.SetTextColor(RGB(255,0,0));				
	hsDC.TextOutW(0,0,t_hscore);
	hsDC.SelectObject(oldFont_h);

}

//HP��`�悷��

void COpengl_GameDlg::drawHp(){

	CWnd* hpBar = GetDlgItem(IDC_HP);
	CWindowDC hpBarDC(hpBar);
	CRect hpBarRECT, drawRECT;
	COLORREF color;
	int w, h;

	hpBar->GetClientRect(hpBarRECT);
	w = hpBarRECT.Width();
	h = hpBarRECT.Height();
	hpBarDC.FillSolidRect( hpBarRECT, RGB( 0, 255, 255));
	drawRECT = hpBarRECT;
	drawRECT.SetRect( w, h, hp*3, 0);
	hpBarDC.FillSolidRect( drawRECT, RGB(255,0,0));

}

//HP��0�ɂȂ�����Ăяo�����
void COpengl_GameDlg::gameOver(){

	gameOverFlg = true;
	startFlg = false;
	timerFlg = false;
	hScore_0 = score;
	timercnt = 0;
	if(hScore_0>hScore_1){
		hScore_1 = hScore_0;
		hScore_0 = 0;
	}else{
		hScore_0 = 0;
	}

	if(gameOverFlg){	

		drawScore();

		DWORD dwExitCode;
		while(1){
			GetExitCodeThread( h_opengl, &dwExitCode);
			GetExitCodeThread( h_timer, &dwExitCode);
			if( dwExitCode != STILL_ACTIVE) break;
		}
		CloseHandle(h_opengl);
		h_opengl = NULL;

		CloseHandle(h_timer);
		h_timer = NULL;

	}
}

//�ԐF���`��
void COpengl_GameDlg::SetRedRender(bool click){
	glPushMatrix();
	if(!click){
		//red

		glTranslatef(floorPos[0][0],floorPos[0][1],floorPos[0][2]);

	}else{

		glTranslatef(floorPos[1][0],floorPos[1][1],floorPos[1][2]);

	}
	glRotatef(90,1.0,0.0,0.0);
	glScalef(1.6,2.0,0.0);
	glBegin(GL_POLYGON);
	glColor3f(colors[0][0],colors[0][1],colors[0][2]);
	glVertex3f(0.5,0.5,0.0);
	glVertex3f(-0.5,0.5,0.0);
	glVertex3f(-0.5,-0.5,0.0);
	glVertex3f(0.5,-0.5,0.0);
	glEnd();

	glPopMatrix();
}

//���F���`��
void COpengl_GameDlg::SetYellowRender(bool click){
	glPushMatrix();
	if(!click){

		glTranslatef(floorPos[2][0],floorPos[2][1],floorPos[2][2]);


	}else{

		glTranslatef(floorPos[3][0],floorPos[3][1],floorPos[3][2]);

	}

	glRotatef(90,1.0,0.0,0.0);
	glScalef(1.8,2.0,0.0);
	glBegin(GL_POLYGON);
	glColor3f(colors[1][0],colors[1][1],colors[1][2]);
	glVertex3f(0.5,0.5,0.0);
	glVertex3f(-0.5,0.5,0.0);
	glVertex3f(-0.5,-0.5,0.0);
	glVertex3f(0.5,-0.5,0.0);
	glEnd();

	glPopMatrix();
}

//�Ώ��`��
void COpengl_GameDlg::SetGreenRender(bool click){
	glPushMatrix();
	if(!click){
		//green

		glTranslatef(floorPos[4][0],floorPos[4][1],floorPos[4][2]);

	}else{

		glTranslatef(floorPos[5][0],floorPos[5][1],floorPos[5][2]);

	}
	glRotatef(90,1.0,0.0,0.0);
	glScalef(1.6,2.0,0.0);
	glBegin(GL_POLYGON);
	glColor3f(colors[2][0],colors[2][1],colors[2][2]);
	glVertex3f(0.5,0.5,0.0);
	glVertex3f(-0.5,0.5,0.0);
	glVertex3f(-0.5,-0.5,0.0);
	glVertex3f(0.5,-0.5,0.0);
	glEnd();

	glPopMatrix();

}

//���F���`��
void COpengl_GameDlg::SetCyanRender(bool click){

	glPushMatrix();
	//�N���b�N����Ȃ��̃|�W�V�����ݒ�
	if(!click){

		glTranslatef(floorPos[6][0],floorPos[6][1],floorPos[6][2]);	 

	}else{		   

		glTranslatef(floorPos[7][0],floorPos[7][1],floorPos[7][2]);	 

	}
	glRotatef(90,1.0,0.0,0.0);
	glScalef(1.8,2.0,0.0);
	glBegin(GL_POLYGON);
	glColor3f(colors[3][0],colors[3][1],colors[3][2]);
	glVertex3f(0.5,0.5,0.0);
	glVertex3f(-0.5,0.5,0.0);
	glVertex3f(-0.5,-0.5,0.0);
	glVertex3f(0.5,-0.5,0.0);
	glEnd();

	glPopMatrix();

}

//���`��
void COpengl_GameDlg::SetBlueRender(bool click){
	glPushMatrix();
	if(!click){
		//blue

		glTranslatef(floorPos[8][0],floorPos[8][1],floorPos[8][2]);

	}else{

		glTranslatef(floorPos[9][0],floorPos[9][1],floorPos[9][2]);

	}
	glRotatef(90,1.0,0.0,0.0);
	glScalef(1.6,2.0,0.0);					   
	glBegin(GL_POLYGON);
	glColor3f(colors[4][0],colors[4][1],colors[4][2]);
	glVertex3f(0.5,0.5,0.0);
	glVertex3f(-0.5,0.5,0.0);
	glVertex3f(-0.5,-0.5,0.0);
	glVertex3f(0.5,-0.5,0.0);
	glEnd();
	glPopMatrix();
}








