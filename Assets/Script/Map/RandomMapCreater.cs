using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTail
{
    bool Onset = false;
    Vector3 OnSetPosition = Vector3.zero;

    public MapTail(Vector3 SetPosition, bool SetMap)
    {
        OnSetPosition = SetPosition;
        Onset = SetMap;
    }
}
//배치될 포지션 값을 저장할 구조체 선언
public struct Coord
{
    public int x;
    public int y;

    public Coord(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}

public class RandomMapCreater : BaseObject {


    //static const int MAXTAILENUM = 6;
    //(테스트용 직접 연결 소스) 프리펩 경로로 리소스 찾는것으로 바꾸어야 함
    //public GameObject NormalTail = null;
    //베이스맵을 public으로 둔건 확인용
    GameObject BaseMap = null;
    GameObject BaseMap_Col = null;
    GameObject BuffTile = null;
    GameObject EnemyTile = null;
    GameObject Enemy_BTile = null;
    GameObject Enemy_CTile = null;
    GameObject Enemy_BOSS_Tile = null;
    GameObject TrapTile = null;
    GameObject Trap_CutterTile = null;
    public Vector2 mapSize;

    private GameObject newTail = null;
    private Transform newTrans = null;

    List<GameObject> MyMap = new List<GameObject>();

    // 타일 위치값을 저장할 리스트
    List<Coord> allTileCoords;

    //저장된 값을 순차적으로 반환하는 Queue
    Queue<Coord> ShuffledTileCoord;

    //시드값을 준다.
    int seed = 10;

    bool IsCreateGiant = false;

    int BuffTileCount = 0;
    int EnemyTileCount = 0;
    int TrapTileCount = 0;
    //int Enemy_B_TileCount = 0;
    //int Trap_CutterTileCount = 0;
    int ObjectCount = 0;
    int TailSet = 0;
    public MapTail ThisMapTile;
    List<MapTail> MapTailSet = new List<MapTail>();
    MapTemplate map = null;
    void Start()
    {
        map = MapManager.Instance.Get();
        // 테스트용으로 임시로 갯수 지정함(
        //개수는 JSON에서 받아오기로 함)
        switch(map.MAP_TYPE)
        {
            case eMapTemplateType.D_TYPE:
                BaseMap = Resources.Load("Prefabs/Map/DungeonMap") as GameObject;
                break;
            case eMapTemplateType.BOSS_ROOM:
                BaseMap = Resources.Load("Prefabs/Map/ForestMap") as GameObject;
                break;
            default:
                break;
        }
        BaseMap_Col = Resources.Load("Prefabs/Map/BaseWall") as GameObject;
        BuffTile = Resources.Load("Prefabs/Map/Statue") as GameObject;
        EnemyTile = Resources.Load("Prefabs/Actor/A_Enemy") as GameObject;
        Enemy_BTile = Resources.Load("Prefabs/Actor/B_Enemy") as GameObject;
        Enemy_CTile = Resources.Load("Prefabs/Actor/C_Enemy") as GameObject;
        Enemy_BOSS_Tile = Resources.Load("Prefabs/Actor/Boss_Enemy") as GameObject;
        TrapTile = Resources.Load("Prefabs/Map/Hole") as GameObject;
        Trap_CutterTile = Resources.Load("Prefabs/Map/Trap_Cutter_OB") as GameObject;
        //SetObjectCount()에서 JSON에서 읽어온 값 설정함.
        //BuffTileCount = 10;
        //EnemyTileCount = 5;
        //TrapTileCount = 10;
        TailSet = 0;
        seed = Random.Range(1, 10);
        //ObjectCount = BuffTileCount + EnemyTileCount + TrapTileCount;
        CreateMap();
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = ShuffledTileCoord.Dequeue();
        ShuffledTileCoord.Enqueue(randomCoord);

        return randomCoord;
    }

    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-mapSize.x / 2 + x, 0, -mapSize.y / 2 + y);
    }

    void SetObjectCount()
    {
        
        //MapTemplate map = MapManager.Instance.Get("5");
        if (map == null)
            Debug.Log("맵 못읽어옴ㅋ");
        BuffTileCount = Random.Range(0, 2);
        if (map.KEY == "21") // 보스방 일시에
            EnemyTileCount = 1;
        else
            EnemyTileCount = 1/*Random.Range(4, 8)*/;
        TrapTileCount = Random.Range(0, 3);
        seed = Random.Range(1, 10);
        ObjectCount = BuffTileCount + EnemyTileCount + TrapTileCount;
    }

    public void CreateMap()
    {
        SetObjectCount();
        Debug.Log("반복 들어오는지 확인");
        Debug.Log(ObjectCount);
        allTileCoords = new List<Coord>();
        //배치할 위치 정보값을 나열한다.
        for(int x = 0; x<mapSize.x; x++)
        {
            for(int y = 0; y < mapSize.y; y++)
            {
				if ((x == mapSize.x/2 && y == mapSize.y/2 )
					|| (x == mapSize.x / 2 && y == mapSize.y/2+1)
					|| (x == mapSize.x / 2 + 1 && y == mapSize.y/2)
					|| (x == mapSize.x / 2 + 1 && y == mapSize.y/2 + 1))
					continue;

				allTileCoords.Add(new Coord(x, y));
            }
        }
        //저장 값을 Random하게 셔플함
        ShuffledTileCoord = new Queue<Coord>(Utility.ShuffleArray(allTileCoords.ToArray(), seed));

        //오브젝트를 생성, 자식에 생성된 오브젝트를 부여한다.
        string holderName = "Generator Map";
        if(transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }
        //새로운 게임 오브젝트를 만든다
        Transform mapHolder = new GameObject(holderName).transform;
        //생성된 오브젝트들의 부모를 "Generator Map"으로 부여한다.
        //기본 맵 생성
        mapHolder.parent = transform;
        newTail = Instantiate(BaseMap);
        newTrans = newTail.transform;
        //맵 생성기의 부모를 제너레이터로 설정함.
        newTrans.parent = mapHolder;

        newTail = Instantiate(BaseMap_Col);
        newTrans = newTail.transform;
        //맵 생성기의 부모를 제너레이터로 설정함.
        newTrans.parent = mapHolder;

        //오브젝트의 숫자만큼 반복하여 생성함. 오브젝트 숫자를 합침.
        for (int i = BuffTileCount; i > 0; i--)
        {

            //큐에 저장된 맵 위치값을 가져옴.
            Coord randomCoord = GetRandomCoord();
            //더이상 배치할 오브젝트가 없으면 종료
            if (BuffTileCount < 0)
            {
                Debug.Log("더이상 불러올 오브젝트가 없는지 확인");
                Debug.Log(BuffTileCount);
                return;
            }
            //x, y받은 두개의 값을 vector3 포멧에 맞게 적용함.
            Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
            if (BuffTileCount >= 0)
            {
                //타일 복사 소스를 넣음(값은 큐에서 가져온 값.)
                newTail = Instantiate(BuffTile, obstaclePosition, Quaternion.identity);
                newTrans = newTail.transform;
                //맵 생성기의 부모를 제너레이터로 설정함.
                newTrans.parent = mapHolder;
            }
        }
        for (int i = EnemyTileCount; i > 0; i--)
        {

            //큐에 저장된 맵 위치값을 가져옴.
            Coord randomCoord = GetRandomCoord();
            //더이상 배치할 오브젝트가 없으면 종료
            if (EnemyTileCount < 0)
            {
                Debug.Log("더이상 불러올 오브젝트가 없는지 확인");
                Debug.Log(EnemyTileCount);
                return;
            }
            //x, y받은 두개의 값을 vector3 포멧에 맞게 적용함.
            Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
            if (EnemyTileCount >= 0)
            {
                if (map.KEY == "21")
                    newTail = Instantiate(Enemy_BOSS_Tile, obstaclePosition, Quaternion.identity);
                else
                {
                    int RandomEnemy = Random.Range(0, 3);//랜덤 적 생성
                    switch (RandomEnemy)
                    {
                        case 0:
                            newTail = Instantiate(EnemyTile, obstaclePosition, Quaternion.identity);
                            break;
                        case 1:
                            newTail = Instantiate(Enemy_BTile, obstaclePosition, Quaternion.identity);
                            break;
                        //C타입으로 바꾸어야함
                        case 2:
                            if (IsCreateGiant)
                                continue;
                            newTail = Instantiate(Enemy_CTile, obstaclePosition, Quaternion.identity);
                            IsCreateGiant = true;
                            break;
                        //보스 타입으로 바꾸어야함
                        default:
                            newTail = Instantiate(EnemyTile, obstaclePosition, Quaternion.identity);
                            break;
                    }
                }
                //타일 복사 소스를 넣음(값은 큐에서 가져온 값.)
                //newTail = Instantiate(EnemyTile, obstaclePosition, Quaternion.identity);
                newTrans = newTail.transform;
                //맵 생성기의 부모를 제너레이터로 설정함.
                newTrans.parent = mapHolder;
            }
        }
        Debug.Log(TrapTileCount+"함정개수");
        for (int i = TrapTileCount; i > 0; i--)
        {

            //큐에 저장된 맵 위치값을 가져옴.
            Coord randomCoord = GetRandomCoord();
            //더이상 배치할 오브젝트가 없으면 종료
            if (TrapTileCount < 0)
            {
                Debug.Log("더이상 불러올 오브젝트가 없는지 확인");
                Debug.Log(TrapTileCount);
                return;
            }
            //x, y받은 두개의 값을 vector3 포멧에 맞게 적용함.
            Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
            if (TrapTileCount >= 0)
            {
                int RandomTrap = Random.Range(0, 2);//랜덤 적 생성
                switch (RandomTrap)
                {
                    case 0:
                        newTail = Instantiate(TrapTile, obstaclePosition, Quaternion.identity);
                        break;
                    case 1:
                        newTail = Instantiate(Trap_CutterTile, obstaclePosition, Quaternion.identity);
                        break;
                    default:
                        newTail = Instantiate(Trap_CutterTile, obstaclePosition, Quaternion.identity);
                        break;
                }
                //타일 복사 소스를 넣음(값은 큐에서 가져온 값.)
                //newTail = Instantiate(TrapTile, obstaclePosition, Quaternion.identity);
                newTrans = newTail.transform;
                //맵 생성기의 부모를 제너레이터로 설정함.
                newTrans.parent = mapHolder;
            }
        }

    }
}
