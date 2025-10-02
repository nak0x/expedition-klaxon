using System;
using UnityEngine;
using Utils;
using Scriptables;
using UnityEditor.Rendering;

public class DifficultyController : SingletonMonoBehaviour<DifficultyController>
{
    public Difficulties difficultyList;
    
    public Difficulty currentDifficulty;
    
     public delegate void OnDifficultyEvent(Difficulty difficulty);
     public OnDifficultyEvent OnSetDifficultyEvent;

     private void Awake()
     {
         GameController.Instance.SetDifficultyEvent += SetDifficulty;
     }

     private void SetDifficulty(string difficultySlug)
     {
         Difficulty difficulty = Difficulties.FindDifficultyBySlug(difficultyList.difficulties, difficultySlug);
         if (difficulty != null)
         {
             this.currentDifficulty = difficulty;
             OnSetDifficultyEvent?.Invoke(difficulty);
         } else
             Debug.LogWarning("DifficultyController: Cannot set difficulty: difficulty not found");
     }
}