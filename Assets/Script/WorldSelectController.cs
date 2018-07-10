using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorldSelectController : MonoBehaviour {
	public static WorldSelectController self;

	public Text worldNameText;
	public Image worldIcon;

	void Awake(){
		self = this;
	}

	public void SetWorldNameAndIcon (int world)	{
		worldNameText.text = GameData._worldName;

		switch (world) {
		case 0: worldIcon.sprite = LevelSelectSceneController.self.jakartaSprite; worldNameText.text=GameData.worldNameList[0]; break;
		case 1: worldIcon.sprite = LevelSelectSceneController.self.jakartaSprite1; worldNameText.text=GameData.worldNameList[1]; break;
		case 2: worldIcon.sprite = LevelSelectSceneController.self.jakartaSprite2; worldNameText.text=GameData.worldNameList[2]; break;

		case 3: worldIcon.sprite = LevelSelectSceneController.self.baliSprite; worldNameText.text=GameData.worldNameList[3]; break;
		case 4: worldIcon.sprite = LevelSelectSceneController.self.baliSprite1; worldNameText.text=GameData.worldNameList[4]; break;
		case 5: worldIcon.sprite = LevelSelectSceneController.self.baliSprite2; worldNameText.text=GameData.worldNameList[5]; break;

		case 6: worldIcon.sprite = LevelSelectSceneController.self.padangSprite; worldNameText.text=GameData.worldNameList[6]; break;
		case 7: worldIcon.sprite = LevelSelectSceneController.self.padangSprite1; worldNameText.text=GameData.worldNameList[7]; break;
		case 8: worldIcon.sprite = LevelSelectSceneController.self.padangSprite2; worldNameText.text=GameData.worldNameList[8]; break;

		case 9: worldIcon.sprite = LevelSelectSceneController.self.torajaSprite; worldNameText.text=GameData.worldNameList[9]; break;
		case 10: worldIcon.sprite = LevelSelectSceneController.self.torajaSprite1; worldNameText.text=GameData.worldNameList[10]; break;
		case 11: worldIcon.sprite = LevelSelectSceneController.self.torajaSprite2; worldNameText.text=GameData.worldNameList[11]; break;

		case 12: worldIcon.sprite = LevelSelectSceneController.self.jogjaSprite; worldNameText.text=GameData.worldNameList[12]; break;
		case 13: worldIcon.sprite = LevelSelectSceneController.self.jogjaSprite1; worldNameText.text=GameData.worldNameList[13]; break;
		case 14: worldIcon.sprite = LevelSelectSceneController.self.jogjaSprite2; worldNameText.text=GameData.worldNameList[14]; break;
		}

//		worldIcon.sprite = GameData._worldSprite;
	}
}
