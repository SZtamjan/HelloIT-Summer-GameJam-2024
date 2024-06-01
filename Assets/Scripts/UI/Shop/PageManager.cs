using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Shop
{
    public class PageManager : MonoBehaviour
    {
        [SerializeField] private List<DoublePage> pages;
        private DoublePage _currentPages;
        private int _currentIndex = 0;

        private void Start()
        {
            _currentPages = new DoublePage();
            LoadPage();
        }

        public void NextPage()
        {
            _currentIndex++;
            LoadPage();
        }

        public void PreviousPage()
        {
            _currentIndex--;
            LoadPage();
        }

        private void LoadPage()
        {
            _currentIndex = Mathf.Clamp(_currentIndex, 0, pages.Count-1);
            
            //wylacz stare
            if (_currentPages.leftPage != null)
            {
                Destroy(_currentPages.leftPage);
                Destroy(_currentPages.rightPage);
            }
            
            //zaladuj nowe
            _currentPages.leftPage = Instantiate(pages[_currentIndex].leftPage, transform);
            _currentPages.rightPage = Instantiate(pages[_currentIndex].rightPage, transform);

            //wlacz nowe
            _currentPages.leftPage.SetActive(true);
            _currentPages.rightPage.SetActive(true);
        }
    }
}