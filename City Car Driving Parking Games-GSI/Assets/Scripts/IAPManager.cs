using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System.Linq;

public class IAPManager : MonoBehaviour, IStoreListener
{
    //[Tooltip("Open This Script and Set IAP Product IDs if changed.")]

    public string HELP = "Open This Script and Set IAP Product IDs if changed.";


    public string Thanks = "";

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    public static IAPManager Instance;

    public static string remove_ad = "remove_ad";
  

    private void Awake()
    {
        if (IAPManager.Instance == null)
        {
            IAPManager.Instance = this;
        }
        else
        {
            if (IAPManager.Instance != this)
                DestroyImmediate(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (m_StoreController == null)
        {
            InitializePurchasing();


        }

        /*
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                //   app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
        */
    }
    private Text IAP1Text;
    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(remove_ad, ProductType.NonConsumable);
       
        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                // Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                // Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            //Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void Purchase_remove_ad()
    {
        BuyProductID(remove_ad);
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            // Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        //Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        //Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, remove_ad, StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("RemoveAd", 1);
        }
      

        else
        {
            // Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        //Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    public void DisableBuyDialogPanels()
    {
        //if (InAppSceneManager.Instance != null)
        //{
        //    InAppSceneManager.Instance.DisableBuyDialogPanels();
        //}
    }
}
