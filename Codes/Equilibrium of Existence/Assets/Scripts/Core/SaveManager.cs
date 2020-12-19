namespace Core
{
    // Şu anda kullanılmıyor, ancak bölüm sayısı arttıkça kayıt amaçlı kullanılacak.

    /*
        public static class SaveManager 
        {
            public static int currentLevel = 1; //Slotların hangi bölüm değerini tuttuğu
            public static string path = Application.persistentDataPath + "/saves.txt"; //Kayıt konumu
            
        }
        
        public static void SaveGame()
        {
            //Debug Log
            //Debug.Log("Game Saved!");
    
            //Karakterlerden verileri topla
            List<CharacterData> characterData = new List<CharacterData>();
            GameObject charObj = PlayerController.Instance.gameObject;
    
            //Kayıt konumunda bir dosya yarat veya dosyanın üstüne yaz
            FileStream stream = new FileStream(path,FileMode.Create);
    
            //Binary format kullan, kimse ne yazdığını okuyamasın
            BinaryFormatter formatter = new BinaryFormatter();
    
            //Yazılacak olan veriyi oluştur
            //UserData data = new UserData(currentLevel, characterData, activeCharacterNo,fountainNo, flagCount);
    
            //Veriyi yaz ve kullanımı sonlandır
            formatter.Serialize(stream,data);
            stream.Close();
        }
    
        public static void LoadGame()
        {
            //Daha önceden kayıt alınmışsa
            if (File.Exists(path)) {
    
                //Debug Log
                Debug.Log("Game Loaded!");
    
                //Kayıt konumundaki dosyayı aç
                FileStream stream = new FileStream(path,FileMode.Open);
    
                //Binary formatta yazılan verileri okumak için binary formatter yarat
                BinaryFormatter formatter = new BinaryFormatter();
    
                //Veriyi deşifre et, ilgili değerlere ata
                UserData data = formatter.Deserialize(stream) as UserData;
                currentLevel = data.currentLevel;
                List<CharacterData> characterData = data.characterData;
    
                //Karakterlerin bilgilerini güncelle
                GameObject activeChar = null;
                Transform charObj;
                for (int i = 0; i < Player.gameController.transform.childCount; i++) {
                    charObj = Player.gameController.transform.GetChild(i);
                    charObj.position = new Vector3 (characterData [i].characterPosition[0],characterData [i].characterPosition[1],characterData [i].characterPosition[2]);
                    //charObj.GetComponent<CharacterVariables>().health = characterData [i].characterHealth;
    
                    //Verilere göre aktif olması gereken karakter bu ise
                    if (i == data.activeCharacter) {
                        activeChar = charObj.gameObject;
                    }
                }
    
                //Kullanımı kapat
                stream.Close();
            }
    
            //Oyun ilk kez çalıştırılmış veya kayıt dosyası silinmişse
            else SaveGame();
        }*/
}