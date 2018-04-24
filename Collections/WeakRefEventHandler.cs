﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOLaboratories.Collections
{
    /// <summary>
    /// Stellt methoden bereit um statische Ereignisse zu abbonieren ohne memory leaks.
    /// <para>Ansprechpartner: Henry de Jongh.</para>
    /// </summary>
    public static class WeakRefEventHandler
    {
        /// <summary>
        /// Abonniert ein statisches Ereignis-Listener und entfernt das Abonnement automatisch, nachdem alle Verweise auf dem Besitzer bereits Garbage Collected wurden.
        /// <para>Ein normales statisches Ereignis z.B. "Application.Idle += meinEvent" kann für einen memory leak sorgen weil das statische Ereignis dieses Objekt für immer am leben hält. Sie wird nie Garbage Collected weil es ein "this" pointer gibt bei der ausführung.</para>
        /// </summary>
        /// <typeparam name="T">Typ des Besitzers.</typeparam>
        /// <param name="owner">Der Besitzer der dieses Ereignis verwendet.</param>
        /// <param name="subscribe">Eine Methode, das einem statischem Ereignis abonniert. z.B. "e => Application.Idle += e".</param>
        /// <param name="unsubscribe">Eine Methode, das einem statischem Ereignis kündigt. z.B. "e => Application.Idle -= e".</param>
        /// <param name="callback">Diese Methode wird aufgerufen sobalt das statische Ereignis aufgerufen wird.<para>WICHTIG: NICHT "this" VERWENDEN! Einen Verweis auf dem Eigentümer wird bereits als Parameter übergeben! Mit "this" enthält diese Methode einen Verweis auf dem Eigentümer und wird somit nie gekündigt!</para></param>
        public static void Subscribe<T>(object owner, Action<EventHandler> subscribe, Action<EventHandler> unsubscribe, Action<T, EventArgs> callback)
        {
            // weak reference to the owner, the "this" of the event.
            WeakReference _owner = new WeakReference(owner);

            EventHandler callbackEventHandler = null;
            callbackEventHandler = (s, e) => {
                if (_owner.Target == null)
                {
                    unsubscribe(callbackEventHandler);
                }
                else
                {
                    callback((T)_owner.Target, e);
                }
            };

            subscribe(callbackEventHandler);
        }

        /// <summary>
        /// Abonniert ein statisches Ereignis-Listener und entfernt das Abonnement automatisch, nachdem alle Verweise auf dem Besitzer bereits Garbage Collected wurden.
        /// <para>Ein normales statisches Ereignis z.B. "Application.Idle += meinEvent" kann für einen memory leak sorgen weil das statische Ereignis dieses Objekt für immer am leben hält. Sie wird nie Garbage Collected weil es ein "this" pointer gibt bei der ausführung.</para>
        /// </summary>
        /// <typeparam name="T">Typ des Besitzers.</typeparam>
        /// <param name="owner">Der Besitzer der dieses Ereignis verwendet.</param>
        /// <param name="subscribe">Eine Methode, das einem statischem Ereignis abonniert. z.B. "e => Application.Idle += e".</param>
        /// <param name="unsubscribe">Eine Methode, das einem statischem Ereignis kündigt. z.B. "e => Application.Idle -= e".</param>
        /// <param name="callback">Diese Methode wird aufgerufen sobalt das statische Ereignis aufgerufen wird.<para>WICHTIG: NICHT "this" VERWENDEN! Einen Verweis auf dem Eigentümer wird bereits als Parameter übergeben! Mit "this" enthält diese Methode einen Verweis auf dem Eigentümer und wird somit nie gekündigt!</para></param>
        public static void Subscribe<T>(object owner, Action<ModalTabControl.IndexChangedEventHandler> subscribe, Action<ModalTabControl.IndexChangedEventHandler> unsubscribe, Action<T, ModalTabControl.IndexChangedEventArgs> callback)
        {
            // weak reference to the owner, the "this" of the event.
            WeakReference _owner = new WeakReference(owner);

            ModalTabControl.IndexChangedEventHandler callbackEventHandler = null;
            callbackEventHandler = (s, e) => {
                if (_owner.Target == null)
                {
                    unsubscribe(callbackEventHandler);
                }
                else
                {
                    callback((T)_owner.Target, e);
                }
            };

            subscribe(callbackEventHandler);
        }
    }
}
