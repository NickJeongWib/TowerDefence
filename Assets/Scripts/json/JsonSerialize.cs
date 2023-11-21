using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace TowerDefence
{
    public static class JsonSerialize
    {
        // ���̺� json
        public static void SavePlayerToJson(GameDataManager gamedatamanager)
        {
            string fileName = Path.Combine(Application.persistentDataPath + "/EconomyData.json");

            // ����� ������ ������ ����
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // EconomyData�� �޸𸮿� �ϳ� ���� �����ڿ� ��ȭ �ν��Ͻ��� �����Ѵ�.
            EconomyData data = new EconomyData(gamedatamanager);
            // data�� json���� ��ȯ
            string json = JsonUtility.ToJson(data);

            // �ϼ��� json string ���ڿ��� 8��Ʈ ��ȣ���� ������ ��ȯ
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
            // ��ȯ�� ����Ʈ�迭�� base-64 ���ڵ��� ���ڿ��� ��ȯ
            string encodedJson = System.Convert.ToBase64String(bytes);

            // ��ȯ�� ���� ����

            // ��ȯ�� ���� ����
            File.WriteAllText(fileName, encodedJson);
        }

        // �ε� Json
        public static void LoadPlayerFromJson(GameDataManager gamedatamanager)
        {
            string fileName = Path.Combine(Application.persistentDataPath + "/EconomyData.json");

            // ������ ���� ���� �ε� �Ѵ�.
            if (File.Exists(fileName))
            {
                // ��ũ���� �о�� string  ������ ��´�.
                string jsonFromFile = File.ReadAllText(fileName);

                // �о�� base-64 ���ڵ� ���ڿ��� ����Ʈ�迭�� ��ȯ
                byte[] bytes = System.Convert.FromBase64String(jsonFromFile);

                // 8��Ʈ ��ȣ���� ������ json ���ڿ��� ��ȯ
                string deencodedJson = System.Text.Encoding.UTF8.GetString(bytes);

                // json ��ƿ��Ƽ�� EconomyData���·� �����Ѵ�.
                EconomyData data = JsonUtility.FromJson<EconomyData>(deencodedJson);

                // ������ data�� ��ȭ�� �����Ϳ� �����Ѵ�.
                gamedatamanager.Gold = data.Gold;
                gamedatamanager.Gem = data.Gem;
                gamedatamanager.isFirstStarter = data.isFirstStarter;
            }
        }
    }
}