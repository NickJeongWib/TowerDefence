using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace TowerDefence
{
    public static class JsonSerialize
    {
        // 세이브 json
        public static void SavePlayerToJson(GameDataManager gamedatamanager)
        {
            string fileName = Path.Combine(Application.persistentDataPath + "/EconomyData.json");

            // 저장된 파일이 있으면 삭제
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // EconomyData를 메모리에 하나 찍어내고 생성자에 재화 인스턴스를 전달한다.
            EconomyData data = new EconomyData(gamedatamanager);
            // data를 json으로 변환
            string json = JsonUtility.ToJson(data);

            // 완성된 json string 문자열을 8비트 부호없는 정수로 변환
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
            // 변환된 바이트배열을 base-64 인코딩된 문자열로 변환
            string encodedJson = System.Convert.ToBase64String(bytes);

            // 변환된 값을 저장

            // 뱐환된 값을 저장
            File.WriteAllText(fileName, encodedJson);
        }

        // 로드 Json
        public static void LoadPlayerFromJson(GameDataManager gamedatamanager)
        {
            string fileName = Path.Combine(Application.persistentDataPath + "/EconomyData.json");

            // 파일이 있을 때만 로드 한다.
            if (File.Exists(fileName))
            {
                // 디스크에서 읽어와 string  변수에 담는다.
                string jsonFromFile = File.ReadAllText(fileName);

                // 읽어온 base-64 인코딩 문자열을 바이트배열로 변환
                byte[] bytes = System.Convert.FromBase64String(jsonFromFile);

                // 8비트 부호없는 정수를 json 문자열로 변환
                string deencodedJson = System.Text.Encoding.UTF8.GetString(bytes);

                // json 유틸리티로 EconomyData형태로 복원한다.
                EconomyData data = JsonUtility.FromJson<EconomyData>(deencodedJson);

                // 복원된 data로 재화의 데이터에 저장한다.
                gamedatamanager.Gold = data.Gold;
                gamedatamanager.Gem = data.Gem;
                gamedatamanager.isFirstStarter = data.isFirstStarter;
            }
        }
    }
}