using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/*    Start
   * -> 10개의 배열에 Cube 생성
   * -> 폭발효과는 오브젝트풀로 설정
   * -> Input UI에서 값을 받아와 Cube에 저장
   * # Stack #
   * -> Pop은 LastIndex에서 실행
   * -> Pop후 DestroyEffect()실행
   * # Queue #
   * -> Dequeue는 Index 0 에서 실행
   * -> Dequeue후 DestroyEffect()실행
   */

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
   
   public GameObject[] Cube;
   public Transform SpawnPositon;
   public static int NodeLenght = 10;
   public GameObject ExplosionEffect;

   
   
   // 변수를 저장하게 될 Node
   private GameObject[] _nodeObjects = new GameObject[NodeLenght];
   private List<GameObject> _expList = new List<GameObject>();
   
   private void Awake()
   {
      Instance = GetComponent<GameManager>();
   }

   private void OnEnable()
   {
      InitNode();
      InitStack();
   }
   // 스택을 저장할 노드를 생성
   void InitNode()
   {
      for (int i = 0; i < NodeLenght; i++)
      {
         int rand = Random.Range(0, Cube.Length);
         GameObject nodeGo = Instantiate(Cube[rand]);
         _nodeObjects[i] = nodeGo;
         nodeGo.SetActive(false);
      }
   }

   void InitStack()
   {
      for (int i = 0; i < 4; i++)
      {
         GameObject expGo = Instantiate(ExplosionEffect);
         _expList.Add(expGo);
         expGo.SetActive(false);
      }
   }
   IEnumerator PoolExplosion(Transform nodePos)
   {
      for (int i = 0; i < _expList.Count; i++)
      {
         if (!_expList[i].activeInHierarchy)
         {
            _expList[i].transform.position = nodePos.position;
            _expList[i].SetActive(true);
            yield return new WaitForSeconds(1f);
            _expList[i].SetActive(false);
            yield break;
         }
      }
      GameObject addGo = Instantiate(ExplosionEffect,nodePos.position,Quaternion.identity);
      _expList.Add(addGo);
      addGo.SetActive(true);
      yield return new WaitForSeconds(1f);
      addGo.SetActive(false);
   }
   /// <summary>
   /// Input New Node to Stack or Queue
   /// </summary>
   /// <param name="index">Push index</param>
   /// <param name="key">Get node key</param>
   public void PushNode(int index,string key)
   {
      _nodeObjects[index].transform.position = SpawnPositon.position;
      _nodeObjects[index].SetActive(true);
      _nodeObjects[index].GetComponentInChildren<TextMeshPro>().text = key;
   }

   public string PopNode(int index)
   {
      StartCoroutine(PoolExplosion(_nodeObjects[index].transform));
      _nodeObjects[index].SetActive(false);
      _nodeObjects[index].transform.position = SpawnPositon.position;

      return _nodeObjects[index].GetComponentInChildren<TextMeshPro>().text;
   }

   public string GetRearQueue()
   {
      return _nodeObjects[0].GetComponentInChildren<TextMeshPro>().text;
   }

   public void EraseLastQueue(int front)
   {
      for(int i=0;i<front;i++)
      {
         _nodeObjects[i].GetComponentInChildren<TextMeshPro>().text = _nodeObjects[i + 1].GetComponentInChildren<TextMeshPro>().text;
      }
      _nodeObjects[front].SetActive(false);
   }
   
   public void GoToQueue()
   {
      SceneManager.LoadScene("ScQueue");
   }
   public void GoToStack()
   {
      SceneManager.LoadScene("ScStack");
   }
}
