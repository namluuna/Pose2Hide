﻿
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity.AttachmentTools;

namespace Spine.Unity.Examples {

	public class MixAndMatchSkinsExample : MonoBehaviour {

		public static MixAndMatchSkinsExample Instance;
        // character skins
        [SpineSkin] public string baseSkin = "skin-base";
		[SpineSkin] public string eyelidsSkin = "eyelids/girly";

		// here we use arrays of strings to be able to cycle between them easily.
		[SpineSkin] public string[] hairSkins = { "hair/brown", "hair/blue", "hair/pink", "hair/short-red", "hair/long-blue-with-scarf" };
		public int activeHairIndex = 0;
		[SpineSkin] public string[] eyesSkins = { "eyes/violet", "eyes/green", "eyes/yellow" };
		public int activeEyesIndex = 0;
		[SpineSkin] public string[] noseSkins = { "nose/short", "nose/long" };
		public int activeNoseIndex = 0;

		// equipment skins
		public enum ItemType {
			Cloth,
			Pants,
			Bag,
			Hat
		}

		[SpineSkin] public string clothesSkin = "clothes/hoodie-orange";
		[SpineSkin] public string pantsSkin = "legs/pants-jeans";
		[SpineSkin] public string bagSkin = "";
		[SpineSkin] public string hatSkin = "accessories/hat-red-yellow";

		SkeletonAnimation skeletonAnimation;
		// This "naked body" skin will likely change only once upon character creation,
		// so we store this combined set of non-equipment Skins for later re-use.
		Skin characterSkin;

		// for repacking the skin to a new atlas texture
		public Material runtimeMaterial;
		public Texture2D runtimeAtlas;

		void Awake () {
			Instance = this;
            skeletonAnimation = this.GetComponent<SkeletonAnimation>();
		}

		void Start () {
			UpdateCharacterSkin();
			UpdateCombinedSkin();
		}

		public void NextHairSkin() {
			activeHairIndex = (activeHairIndex + 1) % hairSkins.Length;
			UpdateCharacterSkin();
			UpdateCombinedSkin();
		}

		public void PrevHairSkin () {
			activeHairIndex = (activeHairIndex + hairSkins.Length - 1) % hairSkins.Length;
			UpdateCharacterSkin();
			UpdateCombinedSkin();
		}

		public void NextEyesSkin () {
			activeEyesIndex = (activeEyesIndex + 1) % eyesSkins.Length;
			UpdateCharacterSkin();
			UpdateCombinedSkin();
		}

		public void PrevEyesSkin () {
			activeEyesIndex = (activeEyesIndex + eyesSkins.Length - 1) % eyesSkins.Length;
			UpdateCharacterSkin();
			UpdateCombinedSkin();
		}

		public void NextNoseSkin () {
			activeNoseIndex = (activeNoseIndex + 1) % noseSkins.Length;
			UpdateCharacterSkin();
			UpdateCombinedSkin();
		}

		public void PrevNoseSkin () {
			activeNoseIndex = (activeNoseIndex + noseSkins.Length - 1) % noseSkins.Length;
			UpdateCharacterSkin();
			UpdateCombinedSkin();
		}

		public void Equip(string itemSkin, ItemType itemType) {
			switch (itemType) {
				case ItemType.Cloth:
					clothesSkin = itemSkin;
					break;
				case ItemType.Pants:
					pantsSkin = itemSkin;
					break;
				case ItemType.Bag:
					bagSkin = itemSkin;
					break;
				case ItemType.Hat:
					hatSkin = itemSkin;
					break;
				default:
					break;
			}
			UpdateCombinedSkin();
		}

		public void OptimizeSkin () {
			// Create a repacked skin.
			var previousSkin = skeletonAnimation.Skeleton.Skin;
			// Note: materials and textures returned by GetRepackedSkin() behave like 'new Texture2D()' and need to be destroyed
			if (runtimeMaterial)
				Destroy(runtimeMaterial);
			if (runtimeAtlas)
				Destroy(runtimeAtlas);
			Skin repackedSkin = previousSkin.GetRepackedSkin("Repacked skin", skeletonAnimation.SkeletonDataAsset.atlasAssets[0].PrimaryMaterial, out runtimeMaterial, out runtimeAtlas);
			previousSkin.Clear();

			// Use the repacked skin.
			skeletonAnimation.Skeleton.Skin = repackedSkin;
			skeletonAnimation.Skeleton.SetSlotsToSetupPose();
			skeletonAnimation.AnimationState.Apply(skeletonAnimation.Skeleton);

			// `GetRepackedSkin()` and each call to `GetRemappedClone()` with parameter `premultiplyAlpha` set to `true`
			// cache necessarily created Texture copies which can be cleared by calling AtlasUtilities.ClearCache().
			// You can optionally clear the textures cache after multiple repack operations.
			// Just be aware that while this cleanup frees up memory, it is also a costly operation
			// and will likely cause a spike in the framerate.
			AtlasUtilities.ClearCache();
			Resources.UnloadUnusedAssets();
		}

		void UpdateCharacterSkin () {
			var skeleton = skeletonAnimation.Skeleton;
			var skeletonData = skeleton.Data;
			characterSkin = new Skin("character-base");
			// Note that the result Skin returned by calls to skeletonData.FindSkin()
			// could be cached once in Start() instead of searching for the same skin
			// every time. For demonstration purposes we keep it simple here.
			characterSkin.AddSkin(skeletonData.FindSkin(baseSkin));
			characterSkin.AddSkin(skeletonData.FindSkin(noseSkins[activeNoseIndex]));
			characterSkin.AddSkin(skeletonData.FindSkin(eyelidsSkin));
			characterSkin.AddSkin(skeletonData.FindSkin(eyesSkins[activeEyesIndex]));
			characterSkin.AddSkin(skeletonData.FindSkin(hairSkins[activeHairIndex]));
		}

		void AddEquipmentSkinsTo (Skin combinedSkin) {
			var skeleton = skeletonAnimation.Skeleton;
			var skeletonData = skeleton.Data;
			combinedSkin.AddSkin(skeletonData.FindSkin(clothesSkin));
			combinedSkin.AddSkin(skeletonData.FindSkin(pantsSkin));
			if (!string.IsNullOrEmpty(bagSkin)) combinedSkin.AddSkin(skeletonData.FindSkin(bagSkin));
			if (!string.IsNullOrEmpty(hatSkin)) combinedSkin.AddSkin(skeletonData.FindSkin(hatSkin));
		}

		void UpdateCombinedSkin () {
			var skeleton = skeletonAnimation.Skeleton;
			var resultCombinedSkin = new Skin("character-combined");

			resultCombinedSkin.AddSkin(characterSkin);
			AddEquipmentSkinsTo(resultCombinedSkin);

			skeleton.SetSkin(resultCombinedSkin);
			skeleton.SetSlotsToSetupPose();
		}
	}
}
