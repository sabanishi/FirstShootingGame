using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public delegate GameObject CREATE_ENEMY_FUNC(TagEnemyStatus enemy_status);

public interface ICommand
{
    void Run();
}

public class CCommandManager
{
    private MGameManager gameManager;
    private List<ICommand> Command = new List<ICommand>();
    public bool isStop;

    private CREATE_ENEMY_FUNC[] EnemyFunc =
       {
        Enemy1.New,Enemy2.New,Enemy3.New,Boss1.New,Enemy4.New,Boss2.New
    };
    string[] EnemyName =
    {
        "Enemy1","Enemy2","Enemy3","Boss1","Enemy4","Boss2"
    };

    public float WaitTime { set; get; }
    public int CommandIndex { set; get; }
    public void Initalize(MGameManager gameManager)
    {
        Command.Clear();
        CommandIndex = 0;
        WaitTime = 0;
        this.gameManager = gameManager;
        this.isStop = false;
    }

    public void Run()
    {
        if (!isStop)
        {
            if (WaitTime > 0)
            {
                WaitTime -= Time.deltaTime;
            }
            else
            {
                WaitTime = 0;
            }
            while (CommandIndex < Command.Count && WaitTime == 0)
            {
                Command[CommandIndex].Run();
                CommandIndex++;
            }
        }
    }

    public bool LoadScript(string file_name)
    {
        CommandIndex = 0;
        WaitTime = 0;

        bool comment = false;
        int loopCount = 0;
        List<string> lines = new List<string>();
        foreach (string line in File.ReadLines(file_name))
        {
            lines.Add(line);
        }
        for(int lineNumber = 0; lineNumber < lines.Count; lineNumber++)
        {
            string line = lines[lineNumber];
            if (string.IsNullOrWhiteSpace(line)) continue;
            if (line.Substring(0, 2) == "//") continue;
            System.StringSplitOptions option = System.StringSplitOptions.RemoveEmptyEntries;
            string[] param = line.Split(new char[] { ',', ' ' }, option);
            if (param.Length <= 0) continue;

            if (param[0] == "/*") comment = true;
            if (param[0] == "*/") comment = false;
      
            if (comment) continue;

            List<string> error_string = new List<string>();
            if (param[0] == "enemy")
            {
                if (param.Length < 2)
                {
                    continue;
                }
                for (int j = 0; j < EnemyName.Length; j++)
                {
                    if (param[1] == EnemyName[j])
                    { 
                        TagEnemyStatus enemy_status = new TagEnemyStatus();
                        for (int k = 2; k < param.Length - 1; k += 2)
                        {
                            if (param[k] == "obj")
                            {
                                enemy_status.EnemyObj = MGameManager.ResourcesLoader.GetObjectHandle(param[k + 1]);
                            }
                            else if (param[k] == "ex")
                            {
                                // 初期値から最終値の倍率を変更する
                                // num	現在の数値(sa～eaの値を入れる)
                                // sa	変化前の初期値
                                // ea	変化前の最終値
                                // sb	変化後の初期値
                                // eb	変化後の最終値
                                // 使用例 10～100までの変化を0～1までの変化に倍率を変更する
                                // cout << ChangeRate( 10, 10, 100, 0, 1 ) << endl;
                                float sax = 0.0f;
                                float eax = 100.0f;
                                float sbx = -7.1f;
                                float ebx = 1.3f;  
                                enemy_status.X = Utils.ChangeRate(float.Parse(param[k + 1]), sax, eax, sbx, ebx);
                            }
                            else if (param[k] == "ey")
                            {
                                float say = 0.0f;
                                float eay = 100.0f;
                                float sby = 3.8f;
                                float eby = -3.8f;
                                enemy_status.Y = Utils.ChangeRate(float.Parse(param[k + 1]), say, eay, sby, eby);
                            }
                            else if (param[k] == "vx")
                            {
                                enemy_status.VX = float.Parse(param[k + 1]);
                            }
                            else if (param[k] == "vy")
                            {
                                enemy_status.VY = float.Parse(param[k + 1]);
                            }
                            else if (param[k] == "hp")
                            {
                                enemy_status.Hp = int.Parse(param[k + 1]);
                            }
                            else if (param[k] == "bl_pattern")
                            {
                                enemy_status.Bulletpattern = (BulletPattern)Enum.Parse(typeof(BulletPattern), param[k + 1]);
                            }
                            else if (param[k] == "bl_color")
                            {
                                enemy_status.Shotcolor = (ShotColor)Enum.Parse(typeof(ShotColor), param[k + 1]);
                            }
                            else if (param[k] == "bl_speed")
                            {
                                enemy_status.BulletSpeed = float.Parse(param[k + 1]);
                            }
                            else if (param[k] == "bl_n_way")
                            {
                                enemy_status.BulletN_Way = int.Parse(param[k + 1]);
                            }
                            else if (param[k] == "bl_range360")
                            {
                                enemy_status.BulletRange360 = float.Parse(param[k + 1]);
                            }
                            else if (param[k] == "bl_interval")
                            {
                                enemy_status.BulletInterval = int.Parse(param[k + 1]);
                            }
                            else if(param[k]=="bl_rotateSpeed")
                            {
                                enemy_status.BulletRotateSpeed = float.Parse(param[k + 1]);
                            }
                            else
                            {
                                Debug.Log("CCommandManagerのLoadScriptのenemy関連の　" + param[k] + "を指定する行が無いよ");
                            }
                        }
                        Command.Add(new CEnemyCreateCommand(EnemyFunc[j], enemy_status));
                    }
                }
            }
            else if (param[0] == "wait")
            {
                if (param.Length < 2)
                { 
                    continue;
                }
                Command.Add(new CWaitCommand(this, float.Parse(param[1])));
            }
            else if (param[0] == "play")
            {
                if (param.Length < 2)
                {
                    continue;
                }
                Command.Add(new CPlayCommand(param[1]));
            }
            else if (param[0] == "fadeout")
            {
                if (param.Length < 4)
                {
                    continue;
                }
                float start_val = float.Parse(param[1]);
                float end_val = float.Parse(param[2]);
                float end_time = float.Parse(param[3]);

                Command.Add(new CFadeOutCommand(start_val, end_val, end_time));
            }
            else if (param[0] == "loop")
            {
                if (loopCount < int.Parse(param[1]))
                {
                    lineNumber -= (int.Parse(param[3])+1);
                    loopCount++;
                }
                else
                {
                    loopCount = 0;
                }
            }
            else if(param[0]=="stop")
            {
                Command.Add(new CStopCommand(this));
            }
        }
        return true;
    }
}

public class CWaitCommand : ICommand
{
    private CCommandManager commandManager;
    private float waitTime;

    public CWaitCommand(CCommandManager manager, float waitTime)
    {
        commandManager = manager;
        this.waitTime = waitTime;
    }
    public void Run()
    {
        commandManager.WaitTime = this.waitTime;
    }
};

 public class CEnemyCreateCommand : ICommand
 {
    private TagEnemyStatus EnemyStatus;
    CREATE_ENEMY_FUNC CreateEnemyFunc;
    public CEnemyCreateCommand(CREATE_ENEMY_FUNC func, TagEnemyStatus enemy_status)
    {
        CreateEnemyFunc = func;
        EnemyStatus = enemy_status;
    }
    public void Run()
    {
       CreateEnemyFunc(EnemyStatus);
    }
};

public class CPlayCommand : ICommand
{
    string BGMName;
    public CPlayCommand(string bgm_name)
    {
        BGMName = bgm_name;
    }
    public void Run()
    {
        //CSoundPlayer.PlaySound(BGMName, false);
    }
};

public class CStopCommand :ICommand
{
    private CCommandManager manager;
    public CStopCommand(CCommandManager manager)
    {
        this.manager = manager;
    }

    public void Run()
    {
        manager.isStop = true;
    }
}

public class CFadeOutCommand : MonoBehaviour, ICommand
{
    float StartVal, EndVal, EndTime;
    public CFadeOutCommand(float start_val, float end_val, float end_time)
    {
        StartVal = start_val;
        EndVal = end_val;
        EndTime = end_time;
    }
    public void Run()
    {
        //fadeout 1 0 5
       // CSoundPlayer.CallSetFadeTimer(StartVal, EndVal, EndTime);
    }
}


