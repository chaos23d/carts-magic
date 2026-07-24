using UnityEngine;
using System;
//using Mycom.Target.Unity.Ads;
/*
using Mycom.Target.Unity.Common;
using Mycom.Target.Unity.Internal;
using Mycom.Target.Unity.Internal.Interfaces;

using Mycom.Target.Unity.Ads;
using Mycom.Target.Unity.Common;
*/
using Object = System.Object;

//using Mycom.TargetUnity.Internal;


public class add 
{
    // Start is called before the first frame update
    //public static InterstitialAd ad;
    public static add add_;
    public static void load_add()
    {
        Debug.Log("load add");
        UInt32 id = 1441341;
        //ad = new InterstitialAd(id);
        //ad.Load();
        //MyTargetManager.DebugMode = true;
        //MyTargetManager.Config = new MyTargetConfig.Builder().WithTestDevices("96a96859-d836-4c07-bb24-d4524406aa90").Build();
        //AndroidJNI.AttachCurrentThread();
        //ad.WithTestDevices("96a96859-d836-4c07-bb24-d4524406aa90");
        //add_ = new add();

        /*
        ad.AdLoadCompleted += add_.OnLoadCompleted;
        ad.AdDismissed += add_.OnAdDismissed;
        ad.AdDisplayed += add_.OnAdDisplayed;
        ad.AdVideoCompleted += add_.OnAdVideoCompleted;
        ad.AdClicked += add_.OnAdClicked;
        ad.AdLoadFailed += add_.OnAdLoadFailed;
        */
        // Запускаем загрузку данных
    }
    
    public static void create_add()
    {


        //ad.Show();
    }
    
    private void OnLoadCompleted(Object sender, EventArgs e)
    {

    }

    private void OnAdDismissed(Object sender, EventArgs e)
    {

    }

    private void OnAdDisplayed(Object sender, EventArgs e)
    {

    }

    private void OnAdVideoCompleted(Object sender, EventArgs e)
    {

    }

    private void OnAdClicked(Object sender, EventArgs e)
    {

    }

    /*
    private void OnAdLoadFailed(Object sender, ErrorEventArgs e)
    {
        Debug.Log("OnAdLoadFailed: " + e.Message);
    }
    
    */
}

/*
namespace Mycom.Target.Unity.Ads
{
    public sealed class InterstitialAd : AbstractAd
    {
        private readonly Object _syncRoot = new Object();
        private readonly UInt32 _slotId;

        private readonly Action _onAdDismissed;
        private readonly Action _onAdDisplayed;
        private readonly Action _onAdVideoCompleted;

        private volatile IInterstitialAdProxy _interstitialAdProxy;

        public InterstitialAd(UInt32 slotId)
        {
            MyTargetLogger.Log("MyTarget.Unity(" + SDKVersion.Version + "): InterstitialAd is created");

            _onAdDismissed = () => _unityDispatcher.Perform(() => AdDismissed?.Invoke(this, EventArgs.Empty));
            _onAdDisplayed = () => _unityDispatcher.Perform(() => AdDisplayed?.Invoke(this, EventArgs.Empty));
            _onAdVideoCompleted = () => _unityDispatcher.Perform(() => AdVideoCompleted?.Invoke(this, EventArgs.Empty));

            _slotId = slotId;
        }

        public void Dismiss() => _platformDispatcher.Perform(() =>
        {
            if (_interstitialAdProxy == null)
            {
                return;
            }

            lock (_syncRoot)
            {
                _interstitialAdProxy?.Dismiss();
            }
        });

        public override void Load() => _platformDispatcher.Perform(() =>
        {
            if (_interstitialAdProxy != null)
            {
                return;
            }

            lock (_syncRoot)
            {
                if (_interstitialAdProxy != null)
                {
                    return;
                }

                _interstitialAdProxy = PlatformFactory.CreateInterstitial(_slotId);

                _customParams.SetCustomParamsProxy(_interstitialAdProxy.CustomParamsProxy);

                _interstitialAdProxy.AdClicked += _onAdClicked;
                _interstitialAdProxy.AdDismissed += _onAdDismissed;
                _interstitialAdProxy.AdDisplayed += _onAdDisplayed;
                _interstitialAdProxy.AdLoadCompleted += _onAdLoadCompleted;
                _interstitialAdProxy.AdLoadFailed += _onAdLoadFailed;
                _interstitialAdProxy.AdVideoCompleted += _onAdVideoCompleted;

                _interstitialAdProxy.Load();
            }
        });

        public void Show() => _platformDispatcher.Perform(() =>
        {
            if (_interstitialAdProxy == null)
            {
                return;
            }

            lock (_syncRoot)
            {
                _interstitialAdProxy?.Show();
            }
        });

        protected override void DisposeImpl()
        {
            if (_interstitialAdProxy == null)
            {
                return;
            }

            lock (_syncRoot)
            {
                if (_interstitialAdProxy == null)
                {
                    return;
                }

                var referenceCopy = _interstitialAdProxy;

                _interstitialAdProxy = null;

                referenceCopy.AdClicked -= _onAdClicked;
                referenceCopy.AdDismissed -= _onAdDismissed;
                referenceCopy.AdDisplayed -= _onAdDisplayed;
                referenceCopy.AdLoadCompleted -= _onAdLoadCompleted;
                referenceCopy.AdLoadFailed -= _onAdLoadFailed;
                referenceCopy.AdVideoCompleted -= _onAdVideoCompleted;

                _platformDispatcher.Perform(referenceCopy.Dispose);
            }
        }

        public event EventHandler AdDismissed;
        public event EventHandler AdDisplayed;
        public event EventHandler AdVideoCompleted;
    }
}
*/